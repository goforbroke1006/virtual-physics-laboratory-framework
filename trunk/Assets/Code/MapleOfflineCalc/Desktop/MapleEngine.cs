using System;
using System.Runtime.InteropServices;

static class MapleEngine
{
    // interface callback definitions
    public delegate void TextCallBack(IntPtr data, int tag, IntPtr output);
    public delegate void ErrorCallBack(IntPtr data, IntPtr offset, IntPtr msg);
    public delegate void StatusCallBack(IntPtr data, IntPtr used, IntPtr alloc, double time);
    public delegate IntPtr ReadLineCallBack(IntPtr data, IntPtr debug);
    public delegate long RedirectCallBack(IntPtr data, IntPtr name, IntPtr mode);
    public delegate IntPtr StreamCallBack(IntPtr data, IntPtr stream, int nargs, IntPtr args);
    public delegate long QueryInterrupt(IntPtr data);
    public delegate IntPtr CallBackCallBack(IntPtr data, IntPtr output);

    public struct MapleCallbacks
    {
        public TextCallBack textCallBack;
        public ErrorCallBack errorCallBack;
        public StatusCallBack statusCallBack;
        public ReadLineCallBack readlineCallBack;
        public RedirectCallBack redirectCallBack;
        public StreamCallBack streamCallBack;
        public QueryInterrupt queryInterrupt;
        public CallBackCallBack callbackCallBack;
    }

    // OpenMaple API methods (there are many more commands in the API,
    // these are just the ones we are using in this example)
    [DllImport(@"maplec.dll")]
    public static extern IntPtr StartMaple(int argc, String[] argv, ref MapleCallbacks cb, IntPtr data, IntPtr info, byte[] err);
    [DllImport(@"maplec.dll")]
    public static extern IntPtr EvalMapleStatement(IntPtr kv, byte[] statement);
    [DllImport(@"maplec.dll")]
    public static extern IntPtr IsMapleStop(IntPtr kv, IntPtr obj);
    [DllImport(@"maplec.dll")]
    public static extern void StopMaple(IntPtr kv);
}