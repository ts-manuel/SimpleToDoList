#!/usr/bin/env bash

set -e
set -o
set -u
set pipefail

arch=
appimage_arch=
target=
case "$RUNTIME" in
    linux-x64)
        arch=amd64
        appimage_arch=x86_64
        target=x86_64;;
    linux-arm64)
        arch=arm64
        appimage_arch=arm_aarch64
        target=aarch64;;
    *)
        echo "Unknown runtime $RUNTIME"
        exit 1;;
esac

APPIMAGETOOL_URL=https://github.com/AppImage/appimagetool/releases/download/continuous/appimagetool-x86_64.AppImage

cd build

if [[ ! -f "appimagetool" ]]; then
    curl -o appimagetool -L "$APPIMAGETOOL_URL"
    chmod +x appimagetool
fi

rm -f SimpleToDoList/*.dbg

mkdir -p SimpleToDoList.AppDir/opt
mkdir -p SimpleToDoList.AppDir/usr/share/metainfo
mkdir -p SimpleToDoList.AppDir/usr/share/applications

cp -r SimpleToDoList SimpleToDoList.AppDir/opt/simpletodolist
desktop-file-install resources/_common/applications/simpletodolist.desktop --dir SimpleToDoList.AppDir/usr/share/applications \
    --set-icon com.simpletodolist_scm.SimpleToDoList --set-key=Exec --set-value=AppRun
mv SimpleToDoList.AppDir/usr/share/applications/{simpletodolist,com.simpletodolist_scm.SimpleToDoList}.desktop
cp resources/_common/icons/simpletodolist.png SimpleToDoList.AppDir/com.simpletodolist_scm.SimpleToDoList.png
ln -rsf SimpleToDoList.AppDir/opt/simpletodolist/simpletodolist SimpleToDoList.AppDir/AppRun
ln -rsf SimpleToDoList.AppDir/usr/share/applications/com.simpletodolist_scm.SimpleToDoList.desktop SimpleToDoList.AppDir
cp resources/appimage/simpletodolist.appdata.xml SimpleToDoList.AppDir/usr/share/metainfo/com.simpletodolist_scm.SimpleToDoList.appdata.xml

ARCH="$appimage_arch" ./appimagetool -v SimpleToDoList.AppDir "simpletodolist-$VERSION.linux.$arch.AppImage"

mkdir -p resources/deb/opt/simpletodolist/
mkdir -p resources/deb/usr/bin
mkdir -p resources/deb/usr/share/applications
mkdir -p resources/deb/usr/share/icons
cp -f SimpleToDoList/* resources/deb/opt/simpletodolist
ln -rsf resources/deb/opt/simpletodolist/simpletodolist resources/deb/usr/bin
cp -r resources/_common/applications resources/deb/usr/share
cp -r resources/_common/icons resources/deb/usr/share
# Calculate installed size in KB
installed_size=$(du -sk resources/deb | cut -f1)
# Update the control file
sed -i -e "s/^Version:.*/Version: $VERSION/" \
    -e "s/^Architecture:.*/Architecture: $arch/" \
    -e "s/^Installed-Size:.*/Installed-Size: $installed_size/" \
    resources/deb/DEBIAN/control
# Build deb package with gzip compression
dpkg-deb -Zgzip --root-owner-group --build resources/deb "simpletodolist_$VERSION-1_$arch.deb"

rpmbuild -bb --target="$target" resources/rpm/SPECS/build.spec --define "_topdir $(pwd)/resources/rpm" --define "_version $VERSION"
mv "resources/rpm/RPMS/$target/simpletodolist-$VERSION-1.$target.rpm" ./
