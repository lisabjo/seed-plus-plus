using System.Runtime.InteropServices;

namespace SeedPlusPlus.Data;

internal static class LibC
{
    private const string PathToLib = "../seed_lib/cmake-build-debug/libseed_lib.dylib";
    
    [DllImport(PathToLib, EntryPoint="init", CharSet=CharSet.Unicode)]
    static extern void Init(string path);
    
    [DllImport(PathToLib, EntryPoint="free_product")]
    public static extern void FreeProduct(IntPtr productPtr);
    
    public static async Task<IntPtr> FindProductById(string dbPath, int id)
    {
        return await Task.Run(() =>
        {
            try
            {
                return _FindProductById(dbPath, id);
            }
            catch (Exception e)
            {
                return IntPtr.Zero;
            }
        });
    }
    [DllImport(PathToLib, EntryPoint="find_product_by_id")]
    private static extern IntPtr _FindProductById(string dbPath, int id);
}
