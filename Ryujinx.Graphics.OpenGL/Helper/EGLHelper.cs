using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Ryujinx.Graphics.OpenGL.Helper
{
    [SupportedOSPlatform("linux")]
    internal static class EGLHelper
    {
        private const string LibraryName = "egl.dll";

        static EGLHelper()
        {
            NativeLibrary.SetDllImportResolver(typeof(EGLHelper).Assembly, (name, assembly, path) =>
            {
                if (name != LibraryName)
                {
                    return IntPtr.Zero;
                }

                if (!NativeLibrary.TryLoad("libEGL.so.1", assembly, path, out IntPtr result))
                {
                    if (!NativeLibrary.TryLoad("libEGL.so", assembly, path, out result))
                    {
                        return IntPtr.Zero;
                    }
                }

                return result;
            });
        }

        [DllImport(LibraryName, EntryPoint = "eglGetCurrentContext")]
        public static extern IntPtr GetCurrentContext();
    }
}
