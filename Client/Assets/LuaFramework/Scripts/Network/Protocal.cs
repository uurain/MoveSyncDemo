
namespace LuaFramework {
    public class Protocal {
        ///BUILD TABLE
        public const int Connect = 101;     //连接服务器
        public const int Exception = 102;     //异常掉线
        public const int Disconnect = 103;     //正常断线   
        public const int Message = 104;     //接受消息 
        public const int ConnectFail = 105;     //连接失败
    }
}