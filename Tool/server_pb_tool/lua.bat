for %%i in (*.proto) do (  
    ..\protoc-gen-lua\example\protoc.exe --plugin=protoc-gen-lua="..\protoc-gen-lua\plugin\build.bat" --lua_out=. %%i      
)
copy .\\*.lua  ..\\..\\Client\\Assets\\LuaFramework\\Lua\\Protocol\\ /Y