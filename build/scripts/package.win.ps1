Remove-Item -Path build\SimpleToDoList\*.pdb -Force
Compress-Archive -Path build\SimpleToDoList -DestinationPath "build\simpletodolist_${env:VERSION}.${env:RUNTIME}.zip" -Force