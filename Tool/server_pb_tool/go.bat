protoc --go_out=. MsgDefine.proto

copy .\\*.go  ..\\src\\leafserver\\src\\server\\msg\\ /Y

pause