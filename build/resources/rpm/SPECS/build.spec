Name: simpletodolist
Version: %_version
Release: 1
Summary: Super Simple To Do List
License: MIT
URL: https://github.com/ts-manuel/SimpleToDoList
Source: https://github.com/ts-manuel/SimpleToDoList/archive/refs/tags/v%_version.tar.gz
Requires: libX11.so.6()(%{__isa_bits}bit)
Requires: libSM.so.6()(%{__isa_bits}bit)
Requires: libicu
Requires: xdg-utils

%define _build_id_links none

%description
Super Simple To Do List

%install
mkdir -p %{buildroot}/opt/simpletodolist
mkdir -p %{buildroot}/%{_bindir}
mkdir -p %{buildroot}/usr/share/applications
mkdir -p %{buildroot}/usr/share/icons
cp -f %{_topdir}/../../SimpleToDoList/* %{buildroot}/opt/simpletodolist/
ln -rsf %{buildroot}/opt/simpletodolist/simpletodolist %{buildroot}/%{_bindir}
cp -r %{_topdir}/../_common/applications %{buildroot}/%{_datadir}
cp -r %{_topdir}/../_common/icons %{buildroot}/%{_datadir}
chmod 755 -R %{buildroot}/opt/simpletodolist
chmod 755 %{buildroot}/%{_datadir}/applications/simpletodolist.desktop

%files
%dir /opt/simpletodolist/
/opt/simpletodolist/*
/usr/share/applications/simpletodolist.desktop
/usr/share/icons/*
%{_bindir}/simpletodolist

%changelog
# skip
