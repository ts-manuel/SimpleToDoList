#!/usr/bin/env bash

set -e
set -o
set -u
set pipefail

cd build

mkdir -p SimpleToDoList.app/Contents/Resources
mv SimpleToDoList SimpleToDoList.app/Contents/MacOS
cp resources/app/App.icns SimpleToDoList.app/Contents/Resources/App.icns
sed "s/SIMPLE_TODO_LIST_VERSION/$VERSION/g" resources/app/App.plist > SimpleToDoList.app/Contents/Info.plist
rm -rf SimpleToDoList.app/Contents/MacOS/SimpleToDoList.dsym

zip "simpletodolist_$VERSION.$RUNTIME.zip" -r SimpleToDoList.app
