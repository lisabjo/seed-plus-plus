using System.Runtime.InteropServices;

namespace SeedPlusPlus.Data;

internal static class LibC
{
    private const string PathToLib = "../c-test-thing/cmake-build-debug/libc_test_thing.dylib";
    
    [DllImport(PathToLib, EntryPoint="insert_it")]
    public static extern void InsertIt();
    
    [DllImport(PathToLib, EntryPoint="hello")]
    static extern void dfgdfg();
    [DllImport(PathToLib, EntryPoint="init", CharSet = CharSet.Unicode)]
    static extern void Init(string path);
}