using System.Runtime.InteropServices;

namespace SeedPlusPlus.Data;

internal static class LibC
{
    private const string PathToLib = "../c-test-thing/cmake-build-debug/libc_test_thing.dylib";
    
    [DllImport(PathToLib, EntryPoint="insert_it")]
    public static extern void InsertIt();
    
    [DllImport(PathToLib, EntryPoint="init", CharSet = CharSet.Unicode)]
    static extern void Init(string path);
    
    [DllImport(PathToLib, EntryPoint="find_product_by_id")]
    public static extern IntPtr FindProductById(string dbPath, int id);
    [DllImport(PathToLib, EntryPoint="free_product")]
    public static extern void FreeProduct(IntPtr productPtr);
}