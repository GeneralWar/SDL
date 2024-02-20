using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace General.SDL
{
    static public unsafe partial class SDL2
    {
        private const string LIBRARY_NAME = "SDL2";
        private const CallingConvention CALLING_CONVENTION = CallingConvention.Cdecl;

        static SDL2()
        {
            NativeLibrary.SetDllImportResolver(typeof(SDL2).Assembly, (string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
            {
                OperatingSystem version = Environment.OSVersion;
                string path;
                switch (version.Platform)
                {
                    case PlatformID.Win32NT:
                        path = $"runtimes/{(Environment.Is64BitProcess ? "win-x64" : "win-x86")}/native/SDL2.dll";
                        break;
                    default: throw new NotImplementedException($"General.SDL: Do not support {version.Platform} currently.");
                }
                return NativeLibrary.Load(path, assembly, searchPath);
            });
        }

        public enum SDL_bool
        {
            FALSE,
            TRUE
        }

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate long SDLRWopsSizeCallback(IntPtr context);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate long SDLRWopsSeekCallback(IntPtr context, long offset, int whence);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate IntPtr SDLRWopsReadCallback(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate IntPtr SDLRWopsWriteCallback(IntPtr context, IntPtr ptr, IntPtr size, IntPtr num);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate int SDLRWopsCloseCallback(IntPtr context);

        public struct RWops
        {
            public IntPtr size;

            public IntPtr seek;

            public IntPtr read;

            public IntPtr write;

            public IntPtr close;

            public uint type;
        }

        public delegate int main_func(int argc, IntPtr argv);

        public enum HintPriority
        {
            HINT_DEFAULT,
            HINT_NORMAL,
            HINT_OVERRIDE
        }

        public enum LogCategory
        {
            LOG_CATEGORY_APPLICATION,
            LOG_CATEGORY_ERROR,
            LOG_CATEGORY_ASSERT,
            LOG_CATEGORY_SYSTEM,
            LOG_CATEGORY_AUDIO,
            LOG_CATEGORY_VIDEO,
            LOG_CATEGORY_RENDER,
            LOG_CATEGORY_INPUT,
            LOG_CATEGORY_TEST,
            LOG_CATEGORY_RESERVED1,
            LOG_CATEGORY_RESERVED2,
            LOG_CATEGORY_RESERVED3,
            LOG_CATEGORY_RESERVED4,
            LOG_CATEGORY_RESERVED5,
            LOG_CATEGORY_RESERVED6,
            LOG_CATEGORY_RESERVED7,
            LOG_CATEGORY_RESERVED8,
            LOG_CATEGORY_RESERVED9,
            LOG_CATEGORY_RESERVED10,
            LOG_CATEGORY_CUSTOM
        }

        public enum LogPriority
        {
            LOG_PRIORITY_VERBOSE = 1,
            LOG_PRIORITY_DEBUG,
            LOG_PRIORITY_INFO,
            LOG_PRIORITY_WARN,
            LOG_PRIORITY_ERROR,
            LOG_PRIORITY_CRITICAL,
            NUM_LOG_PRIORITIES
        }

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate void LogOutputFunction(IntPtr userdata, int category, LogPriority priority, IntPtr message);

        [Flags]
        public enum MessageBoxFlags : uint
        {
            MESSAGEBOX_ERROR = 0x10u,
            MESSAGEBOX_WARNING = 0x20u,
            MESSAGEBOX_INFORMATION = 0x40u
        }

        [Flags]
        public enum MessageBoxButtonFlags : uint
        {
            MESSAGEBOX_BUTTON_RETURNKEY_DEFAULT = 0x1u,
            MESSAGEBOX_BUTTON_ESCAPEKEY_DEFAULT = 0x2u
        }

        private struct INTERNAL_MessageBoxButtonData
        {
            public MessageBoxButtonFlags flags;

            public int buttonid;

            public IntPtr text;
        }

        public struct MessageBoxButtonData
        {
            public MessageBoxButtonFlags flags;

            public int buttonid;

            public string text;
        }

        public struct MessageBoxColor
        {
            public byte r;

            public byte g;

            public byte b;
        }

        public enum MessageBoxColorType
        {
            MESSAGEBOX_COLOR_BACKGROUND,
            MESSAGEBOX_COLOR_TEXT,
            MESSAGEBOX_COLOR_BUTTON_BORDER,
            MESSAGEBOX_COLOR_BUTTON_BACKGROUND,
            MESSAGEBOX_COLOR_BUTTON_SELECTED,
            MESSAGEBOX_COLOR_MAX
        }

        public struct MessageBoxColorScheme
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.Struct)]
            public MessageBoxColor[] colors;
        }

        private struct INTERNAL_MessageBoxData
        {
            public MessageBoxFlags flags;

            public IntPtr window;

            public IntPtr title;

            public IntPtr message;

            public int numbuttons;

            public IntPtr buttons;

            public IntPtr colorScheme;
        }

        public struct MessageBoxData
        {
            public MessageBoxFlags flags;

            public IntPtr window;

            public string title;

            public string message;

            public int numbuttons;

            public MessageBoxButtonData[] buttons;

            public MessageBoxColorScheme? colorScheme;
        }

        public struct version
        {
            public byte major;

            public byte minor;

            public byte patch;
        }

        public enum GLattr
        {
            GL_RED_SIZE,
            GL_GREEN_SIZE,
            GL_BLUE_SIZE,
            GL_ALPHA_SIZE,
            GL_BUFFER_SIZE,
            GL_DOUBLEBUFFER,
            GL_DEPTH_SIZE,
            GL_STENCIL_SIZE,
            GL_ACCUM_RED_SIZE,
            GL_ACCUM_GREEN_SIZE,
            GL_ACCUM_BLUE_SIZE,
            GL_ACCUM_ALPHA_SIZE,
            GL_STEREO,
            GL_MULTISAMPLEBUFFERS,
            GL_MULTISAMPLESAMPLES,
            GL_ACCELERATED_VISUAL,
            GL_RETAINED_BACKING,
            GL_CONTEXT_MAJOR_VERSION,
            GL_CONTEXT_MINOR_VERSION,
            GL_CONTEXT_EGL,
            GL_CONTEXT_FLAGS,
            GL_CONTEXT_PROFILE_MASK,
            GL_SHARE_WITH_CURRENT_CONTEXT,
            GL_FRAMEBUFFER_SRGB_CAPABLE,
            GL_CONTEXT_RELEASE_BEHAVIOR,
            GL_CONTEXT_RESET_NOTIFICATION,
            GL_CONTEXT_NO_ERROR
        }

        [Flags]
        public enum GLprofile
        {
            GL_CONTEXT_PROFILE_CORE = 0x1,
            GL_CONTEXT_PROFILE_COMPATIBILITY = 0x2,
            GL_CONTEXT_PROFILE_ES = 0x4
        }

        [Flags]
        public enum GLcontext
        {
            GL_CONTEXT_DEBUG_FLAG = 0x1,
            GL_CONTEXT_FORWARD_COMPATIBLE_FLAG = 0x2,
            GL_CONTEXT_ROBUST_ACCESS_FLAG = 0x4,
            GL_CONTEXT_RESET_ISOLATION_FLAG = 0x8
        }

        public enum WindowEventID : byte
        {
            WINDOWEVENT_NONE,
            WINDOWEVENT_SHOWN,
            WINDOWEVENT_HIDDEN,
            WINDOWEVENT_EXPOSED,
            WINDOWEVENT_MOVED,
            WINDOWEVENT_RESIZED,
            WINDOWEVENT_SIZE_CHANGED,
            WINDOWEVENT_MINIMIZED,
            WINDOWEVENT_MAXIMIZED,
            WINDOWEVENT_RESTORED,
            WINDOWEVENT_ENTER,
            WINDOWEVENT_LEAVE,
            WINDOWEVENT_FOCUS_GAINED,
            WINDOWEVENT_FOCUS_LOST,
            WINDOWEVENT_CLOSE,
            WINDOWEVENT_TAKE_FOCUS,
            WINDOWEVENT_HIT_TEST
        }

        public enum DisplayEventID : byte
        {
            DISPLAYEVENT_NONE,
            DISPLAYEVENT_ORIENTATION,
            DISPLAYEVENT_CONNECTED,
            DISPLAYEVENT_DISCONNECTED
        }

        public enum DisplayOrientation
        {
            ORIENTATION_UNKNOWN,
            ORIENTATION_LANDSCAPE,
            ORIENTATION_LANDSCAPE_FLIPPED,
            ORIENTATION_PORTRAIT,
            ORIENTATION_PORTRAIT_FLIPPED
        }

        [Flags]
        public enum WindowFlags : uint
        {
            WINDOW_FULLSCREEN = 0x1u,
            WINDOW_OPENGL = 0x2u,
            WINDOW_SHOWN = 0x4u,
            WINDOW_HIDDEN = 0x8u,
            WINDOW_BORDERLESS = 0x10u,
            WINDOW_RESIZABLE = 0x20u,
            WINDOW_MINIMIZED = 0x40u,
            WINDOW_MAXIMIZED = 0x80u,
            WINDOW_INPUT_GRABBED = 0x100u,
            WINDOW_INPUT_FOCUS = 0x200u,
            WINDOW_MOUSE_FOCUS = 0x400u,
            WINDOW_FULLSCREEN_DESKTOP = 0x1001u,
            WINDOW_FOREIGN = 0x800u,
            WINDOW_ALLOW_HIGHDPI = 0x2000u,
            WINDOW_MOUSE_CAPTURE = 0x4000u,
            WINDOW_ALWAYS_ON_TOP = 0x8000u,
            WINDOW_SKIP_TASKBAR = 0x10000u,
            WINDOW_UTILITY = 0x20000u,
            WINDOW_TOOLTIP = 0x40000u,
            WINDOW_POPUP_MENU = 0x80000u,
            WINDOW_VULKAN = 0x10000000u,
            WINDOW_METAL = 0x2000000u
        }

        public enum HitTestResult
        {
            HITTEST_NORMAL,
            HITTEST_DRAGGABLE,
            HITTEST_RESIZE_TOPLEFT,
            HITTEST_RESIZE_TOP,
            HITTEST_RESIZE_TOPRIGHT,
            HITTEST_RESIZE_RIGHT,
            HITTEST_RESIZE_BOTTOMRIGHT,
            HITTEST_RESIZE_BOTTOM,
            HITTEST_RESIZE_BOTTOMLEFT,
            HITTEST_RESIZE_LEFT
        }

        public struct DisplayMode
        {
            public uint format;

            public int w;

            public int h;

            public int refresh_rate;

            public IntPtr driverdata;
        }

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate HitTestResult HitTest(IntPtr win, IntPtr area, IntPtr data);

        [Flags]
        public enum BlendMode
        {
            BLENDMODE_NONE = 0x0,
            BLENDMODE_BLEND = 0x1,
            BLENDMODE_ADD = 0x2,
            BLENDMODE_MOD = 0x4,
            BLENDMODE_MUL = 0x8,
            BLENDMODE_INVALID = int.MaxValue
        }

        public enum BlendOperation
        {
            BLENDOPERATION_ADD = 1,
            BLENDOPERATION_SUBTRACT,
            BLENDOPERATION_REV_SUBTRACT,
            BLENDOPERATION_MINIMUM,
            BLENDOPERATION_MAXIMUM
        }

        public enum BlendFactor
        {
            BLENDFACTOR_ZERO = 1,
            BLENDFACTOR_ONE,
            BLENDFACTOR_SRC_COLOR,
            BLENDFACTOR_ONE_MINUS_SRC_COLOR,
            BLENDFACTOR_SRC_ALPHA,
            BLENDFACTOR_ONE_MINUS_SRC_ALPHA,
            BLENDFACTOR_DST_COLOR,
            BLENDFACTOR_ONE_MINUS_DST_COLOR,
            BLENDFACTOR_DST_ALPHA,
            BLENDFACTOR_ONE_MINUS_DST_ALPHA
        }

        [Flags]
        public enum RendererFlags : uint
        {
            RENDERER_SOFTWARE = 0x1u,
            RENDERER_ACCELERATED = 0x2u,
            RENDERER_PRESENTVSYNC = 0x4u,
            RENDERER_TARGETTEXTURE = 0x8u
        }

        [Flags]
        public enum RendererFlip
        {
            FLIP_NONE = 0x0,
            FLIP_HORIZONTAL = 0x1,
            FLIP_VERTICAL = 0x2
        }

        public enum TextureAccess
        {
            TEXTUREACCESS_STATIC,
            TEXTUREACCESS_STREAMING,
            TEXTUREACCESS_TARGET
        }

        [Flags]
        public enum TextureModulate
        {
            TEXTUREMODULATE_NONE = 0x0,
            TEXTUREMODULATE_HORIZONTAL = 0x1,
            TEXTUREMODULATE_VERTICAL = 0x2
        }

        public struct RendererInfo
        {
            public IntPtr name;

            public uint flags;

            public uint num_texture_formats;

            public unsafe fixed uint texture_formats[16];

            public int max_texture_width;

            public int max_texture_height;
        }

        public enum ScaleMode
        {
            ScaleModeNearest,
            ScaleModeLinear,
            ScaleModeBest
        }

        public enum PixelType
        {
            PIXELTYPE_UNKNOWN,
            PIXELTYPE_INDEX1,
            PIXELTYPE_INDEX4,
            PIXELTYPE_INDEX8,
            PIXELTYPE_PACKED8,
            PIXELTYPE_PACKED16,
            PIXELTYPE_PACKED32,
            PIXELTYPE_ARRAYU8,
            PIXELTYPE_ARRAYU16,
            PIXELTYPE_ARRAYU32,
            PIXELTYPE_ARRAYF16,
            PIXELTYPE_ARRAYF32
        }

        public enum BitmapOrder
        {
            BITMAPORDER_NONE,
            BITMAPORDER_4321,
            BITMAPORDER_1234
        }

        public enum PackedOrder
        {
            PACKEDORDER_NONE,
            PACKEDORDER_XRGB,
            PACKEDORDER_RGBX,
            PACKEDORDER_ARGB,
            PACKEDORDER_RGBA,
            PACKEDORDER_XBGR,
            PACKEDORDER_BGRX,
            PACKEDORDER_ABGR,
            PACKEDORDER_BGRA
        }

        public enum ArrayOrder
        {
            ARRAYORDER_NONE,
            ARRAYORDER_RGB,
            ARRAYORDER_RGBA,
            ARRAYORDER_ARGB,
            ARRAYORDER_BGR,
            ARRAYORDER_BGRA,
            ARRAYORDER_ABGR
        }

        public enum PackedLayout
        {
            PACKEDLAYOUT_NONE,
            PACKEDLAYOUT_332,
            PACKEDLAYOUT_4444,
            PACKEDLAYOUT_1555,
            PACKEDLAYOUT_5551,
            PACKEDLAYOUT_565,
            PACKEDLAYOUT_8888,
            PACKEDLAYOUT_2101010,
            PACKEDLAYOUT_1010102
        }

        public struct Color
        {
            public byte r;

            public byte g;

            public byte b;

            public byte a;
        }

        public struct Palette
        {
            public int ncolors;

            public IntPtr colors;

            public int version;

            public int refcount;
        }

        public struct PixelFormat
        {
            public uint format;

            public IntPtr palette;

            public byte BitsPerPixel;

            public byte BytesPerPixel;

            public uint Rmask;

            public uint Gmask;

            public uint Bmask;

            public uint Amask;

            public byte Rloss;

            public byte Gloss;

            public byte Bloss;

            public byte Aloss;

            public byte Rshift;

            public byte Gshift;

            public byte Bshift;

            public byte Ashift;

            public int refcount;

            public IntPtr next;
        }

        public struct Point
        {
            public int x;

            public int y;
        }

        public struct Rect
        {
            public int x;

            public int y;

            public int w;

            public int h;
        }

        public struct FPoint
        {
            public float x;

            public float y;
        }

        public struct FRect
        {
            public float x;

            public float y;

            public float w;

            public float h;
        }

        public struct Surface
        {
            public uint flags;

            public IntPtr format;

            public int w;

            public int h;

            public int pitch;

            public IntPtr pixels;

            public IntPtr userdata;

            public int locked;

            public IntPtr list_blitmap;

            public Rect clip_rect;

            public IntPtr map;

            public int refcount;
        }

        public enum EventType : uint
        {
            FIRSTEVENT = 0u,
            QUIT = 0x100u,
            APP_TERMINATING = 257u,
            APP_LOWMEMORY = 258u,
            APP_WILLENTERBACKGROUND = 259u,
            APP_DIDENTERBACKGROUND = 260u,
            APP_WILLENTERFOREGROUND = 261u,
            APP_DIDENTERFOREGROUND = 262u,
            LOCALECHANGED = 263u,
            DISPLAYEVENT = 336u,
            WINDOWEVENT = 0x200u,
            SYSWMEVENT = 513u,
            KEYDOWN = 768u,
            KEYUP = 769u,
            TEXTEDITING = 770u,
            TEXTINPUT = 771u,
            KEYMAPCHANGED = 772u,
            MOUSEMOTION = 0x400u,
            MOUSEBUTTONDOWN = 1025u,
            MOUSEBUTTONUP = 1026u,
            MOUSEWHEEL = 1027u,
            JOYAXISMOTION = 1536u,
            JOYBALLMOTION = 1537u,
            JOYHATMOTION = 1538u,
            JOYBUTTONDOWN = 1539u,
            JOYBUTTONUP = 1540u,
            JOYDEVICEADDED = 1541u,
            JOYDEVICEREMOVED = 1542u,
            CONTROLLERAXISMOTION = 1616u,
            CONTROLLERBUTTONDOWN = 1617u,
            CONTROLLERBUTTONUP = 1618u,
            CONTROLLERDEVICEADDED = 1619u,
            CONTROLLERDEVICEREMOVED = 1620u,
            CONTROLLERDEVICEREMAPPED = 1621u,
            CONTROLLERTOUCHPADDOWN = 1622u,
            CONTROLLERTOUCHPADMOTION = 1623u,
            CONTROLLERTOUCHPADUP = 1624u,
            CONTROLLERSENSORUPDATE = 1625u,
            FINGERDOWN = 1792u,
            FINGERUP = 1793u,
            FINGERMOTION = 1794u,
            DOLLARGESTURE = 0x800u,
            DOLLARRECORD = 2049u,
            MULTIGESTURE = 2050u,
            CLIPBOARDUPDATE = 2304u,
            DROPFILE = 0x1000u,
            DROPTEXT = 4097u,
            DROPBEGIN = 4098u,
            DROPCOMPLETE = 4099u,
            AUDIODEVICEADDED = 4352u,
            AUDIODEVICEREMOVED = 4353u,
            SENSORUPDATE = 4608u,
            RENDER_TARGETS_RESET = 0x2000u,
            RENDER_DEVICE_RESET = 8193u,
            USEREVENT = 0x8000u,
            LASTEVENT = 0xFFFFu
        }

        public enum MouseWheelDirection : uint
        {
            MOUSEWHEEL_NORMAL,
            MOUSEWHEEL_FLIPPED
        }

        public struct GenericEvent
        {
            public EventType type;

            public uint timestamp;
        }

        public struct DisplayEvent
        {
            public EventType type;

            public uint timestamp;

            public uint display;

            public DisplayEventID displayEvent;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public int data1;
        }

        public struct WindowEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public WindowEventID windowEvent;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public int data1;

            public int data2;
        }

        public struct KeyboardEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public byte state;

            public byte repeat;

            private byte padding2;

            private byte padding3;

            public Keysym keysym;
        }

        public struct TextEditingEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public unsafe fixed byte text[32];

            public int start;

            public int length;
        }

        public struct TextInputEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public unsafe fixed byte text[32];
        }

        public struct MouseMotionEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public uint which;

            public byte state;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public int x;

            public int y;

            public int xrel;

            public int yrel;
        }

        public struct MouseButtonEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public uint which;

            public byte button;

            public byte state;

            public byte clicks;

            private byte padding1;

            public int x;

            public int y;
        }

        public struct MouseWheelEvent
        {
            public EventType type;

            public uint timestamp;

            public uint windowID;

            public uint which;

            public int x;

            public int y;

            public uint direction;
        }

        public struct JoyAxisEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte axis;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public short axisValue;

            public ushort padding4;
        }

        public struct JoyBallEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte ball;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public short xrel;

            public short yrel;
        }

        public struct JoyHatEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte hat;

            public byte hatValue;

            private byte padding1;

            private byte padding2;
        }

        public struct JoyButtonEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte button;

            public byte state;

            private byte padding1;

            private byte padding2;
        }

        public struct JoyDeviceEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;
        }

        public struct ControllerAxisEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte axis;

            private byte padding1;

            private byte padding2;

            private byte padding3;

            public short axisValue;

            private ushort padding4;
        }

        public struct ControllerButtonEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public byte button;

            public byte state;

            private byte padding1;

            private byte padding2;
        }

        public struct ControllerDeviceEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;
        }

        public struct ControllerTouchpadEvent
        {
            public uint type;

            public uint timestamp;

            public int which;

            public int touchpad;

            public int finger;

            public float x;

            public float y;

            public float pressure;
        }

        public struct ControllerSensorEvent
        {
            public uint type;

            public uint timestamp;

            public int which;

            public int sensor;

            public float data1;

            public float data2;

            public float data3;
        }

        public struct AudioDeviceEvent
        {
            public uint type;

            public uint timestamp;

            public uint which;

            public byte iscapture;

            private byte padding1;

            private byte padding2;

            private byte padding3;
        }

        public struct TouchFingerEvent
        {
            public uint type;

            public uint timestamp;

            public long touchId;

            public long fingerId;

            public float x;

            public float y;

            public float dx;

            public float dy;

            public float pressure;

            public uint windowID;
        }

        public struct MultiGestureEvent
        {
            public uint type;

            public uint timestamp;

            public long touchId;

            public float dTheta;

            public float dDist;

            public float x;

            public float y;

            public ushort numFingers;

            public ushort padding;
        }

        public struct DollarGestureEvent
        {
            public uint type;

            public uint timestamp;

            public long touchId;

            public long gestureId;

            public uint numFingers;

            public float error;

            public float x;

            public float y;
        }

        public struct DropEvent
        {
            public EventType type;

            public uint timestamp;

            public IntPtr file;

            public uint windowID;
        }

        public struct SensorEvent
        {
            public EventType type;

            public uint timestamp;

            public int which;

            public unsafe fixed float data[6];
        }

        public struct QuitEvent
        {
            public EventType type;

            public uint timestamp;
        }

        public struct UserEvent
        {
            public uint type;

            public uint timestamp;

            public uint windowID;

            public int code;

            public IntPtr data1;

            public IntPtr data2;
        }

        public struct SysWMEvent
        {
            public EventType type;

            public uint timestamp;

            public IntPtr msg;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct Event
        {
            [FieldOffset(0)]
            public EventType type;

            [FieldOffset(0)]
            public EventType typeFSharp;

            [FieldOffset(0)]
            public DisplayEvent display;

            [FieldOffset(0)]
            public WindowEvent window;

            [FieldOffset(0)]
            public KeyboardEvent key;

            [FieldOffset(0)]
            public TextEditingEvent edit;

            [FieldOffset(0)]
            public TextInputEvent text;

            [FieldOffset(0)]
            public MouseMotionEvent motion;

            [FieldOffset(0)]
            public MouseButtonEvent button;

            [FieldOffset(0)]
            public MouseWheelEvent wheel;

            [FieldOffset(0)]
            public JoyAxisEvent jaxis;

            [FieldOffset(0)]
            public JoyBallEvent jball;

            [FieldOffset(0)]
            public JoyHatEvent jhat;

            [FieldOffset(0)]
            public JoyButtonEvent jbutton;

            [FieldOffset(0)]
            public JoyDeviceEvent jdevice;

            [FieldOffset(0)]
            public ControllerAxisEvent caxis;

            [FieldOffset(0)]
            public ControllerButtonEvent cbutton;

            [FieldOffset(0)]
            public ControllerDeviceEvent cdevice;

            [FieldOffset(0)]
            public ControllerDeviceEvent ctouchpad;

            [FieldOffset(0)]
            public ControllerDeviceEvent csensor;

            [FieldOffset(0)]
            public AudioDeviceEvent adevice;

            [FieldOffset(0)]
            public SensorEvent sensor;

            [FieldOffset(0)]
            public QuitEvent quit;

            [FieldOffset(0)]
            public UserEvent user;

            [FieldOffset(0)]
            public SysWMEvent syswm;

            [FieldOffset(0)]
            public TouchFingerEvent tfinger;

            [FieldOffset(0)]
            public MultiGestureEvent mgesture;

            [FieldOffset(0)]
            public DollarGestureEvent dgesture;

            [FieldOffset(0)]
            public DropEvent drop;

            [FieldOffset(0)]
            private unsafe fixed byte padding[56];
        }

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate int EventFilter(IntPtr userdata, IntPtr sdlevent);

        public enum eventaction
        {
            ADDEVENT,
            PEEKEVENT,
            GETEVENT
        }

        public enum Scancode
        {
            SCANCODE_UNKNOWN = 0,
            SCANCODE_A = 4,
            SCANCODE_B = 5,
            SCANCODE_C = 6,
            SCANCODE_D = 7,
            SCANCODE_E = 8,
            SCANCODE_F = 9,
            SCANCODE_G = 10,
            SCANCODE_H = 11,
            SCANCODE_I = 12,
            SCANCODE_J = 13,
            SCANCODE_K = 14,
            SCANCODE_L = 0xF,
            SCANCODE_M = 0x10,
            SCANCODE_N = 17,
            SCANCODE_O = 18,
            SCANCODE_P = 19,
            SCANCODE_Q = 20,
            SCANCODE_R = 21,
            SCANCODE_S = 22,
            SCANCODE_T = 23,
            SCANCODE_U = 24,
            SCANCODE_V = 25,
            SCANCODE_W = 26,
            SCANCODE_X = 27,
            SCANCODE_Y = 28,
            SCANCODE_Z = 29,
            SCANCODE_1 = 30,
            SCANCODE_2 = 0x1F,
            SCANCODE_3 = 0x20,
            SCANCODE_4 = 33,
            SCANCODE_5 = 34,
            SCANCODE_6 = 35,
            SCANCODE_7 = 36,
            SCANCODE_8 = 37,
            SCANCODE_9 = 38,
            SCANCODE_0 = 39,
            SCANCODE_RETURN = 40,
            SCANCODE_ESCAPE = 41,
            SCANCODE_BACKSPACE = 42,
            SCANCODE_TAB = 43,
            SCANCODE_SPACE = 44,
            SCANCODE_MINUS = 45,
            SCANCODE_EQUALS = 46,
            SCANCODE_LEFTBRACKET = 47,
            SCANCODE_RIGHTBRACKET = 48,
            SCANCODE_BACKSLASH = 49,
            SCANCODE_NONUSHASH = 50,
            SCANCODE_SEMICOLON = 51,
            SCANCODE_APOSTROPHE = 52,
            SCANCODE_GRAVE = 53,
            SCANCODE_COMMA = 54,
            SCANCODE_PERIOD = 55,
            SCANCODE_SLASH = 56,
            SCANCODE_CAPSLOCK = 57,
            SCANCODE_F1 = 58,
            SCANCODE_F2 = 59,
            SCANCODE_F3 = 60,
            SCANCODE_F4 = 61,
            SCANCODE_F5 = 62,
            SCANCODE_F6 = 0x3F,
            SCANCODE_F7 = 0x40,
            SCANCODE_F8 = 65,
            SCANCODE_F9 = 66,
            SCANCODE_F10 = 67,
            SCANCODE_F11 = 68,
            SCANCODE_F12 = 69,
            SCANCODE_PRINTSCREEN = 70,
            SCANCODE_SCROLLLOCK = 71,
            SCANCODE_PAUSE = 72,
            SCANCODE_INSERT = 73,
            SCANCODE_HOME = 74,
            SCANCODE_PAGEUP = 75,
            SCANCODE_DELETE = 76,
            SCANCODE_END = 77,
            SCANCODE_PAGEDOWN = 78,
            SCANCODE_RIGHT = 79,
            SCANCODE_LEFT = 80,
            SCANCODE_DOWN = 81,
            SCANCODE_UP = 82,
            SCANCODE_NUMLOCKCLEAR = 83,
            SCANCODE_KP_DIVIDE = 84,
            SCANCODE_KP_MULTIPLY = 85,
            SCANCODE_KP_MINUS = 86,
            SCANCODE_KP_PLUS = 87,
            SCANCODE_KP_ENTER = 88,
            SCANCODE_KP_1 = 89,
            SCANCODE_KP_2 = 90,
            SCANCODE_KP_3 = 91,
            SCANCODE_KP_4 = 92,
            SCANCODE_KP_5 = 93,
            SCANCODE_KP_6 = 94,
            SCANCODE_KP_7 = 95,
            SCANCODE_KP_8 = 96,
            SCANCODE_KP_9 = 97,
            SCANCODE_KP_0 = 98,
            SCANCODE_KP_PERIOD = 99,
            SCANCODE_NONUSBACKSLASH = 100,
            SCANCODE_APPLICATION = 101,
            SCANCODE_POWER = 102,
            SCANCODE_KP_EQUALS = 103,
            SCANCODE_F13 = 104,
            SCANCODE_F14 = 105,
            SCANCODE_F15 = 106,
            SCANCODE_F16 = 107,
            SCANCODE_F17 = 108,
            SCANCODE_F18 = 109,
            SCANCODE_F19 = 110,
            SCANCODE_F20 = 111,
            SCANCODE_F21 = 112,
            SCANCODE_F22 = 113,
            SCANCODE_F23 = 114,
            SCANCODE_F24 = 115,
            SCANCODE_EXECUTE = 116,
            SCANCODE_HELP = 117,
            SCANCODE_MENU = 118,
            SCANCODE_SELECT = 119,
            SCANCODE_STOP = 120,
            SCANCODE_AGAIN = 121,
            SCANCODE_UNDO = 122,
            SCANCODE_CUT = 123,
            SCANCODE_COPY = 124,
            SCANCODE_PASTE = 125,
            SCANCODE_FIND = 126,
            SCANCODE_MUTE = 0x7F,
            SCANCODE_VOLUMEUP = 0x80,
            SCANCODE_VOLUMEDOWN = 129,
            SCANCODE_KP_COMMA = 133,
            SCANCODE_KP_EQUALSAS400 = 134,
            SCANCODE_INTERNATIONAL1 = 135,
            SCANCODE_INTERNATIONAL2 = 136,
            SCANCODE_INTERNATIONAL3 = 137,
            SCANCODE_INTERNATIONAL4 = 138,
            SCANCODE_INTERNATIONAL5 = 139,
            SCANCODE_INTERNATIONAL6 = 140,
            SCANCODE_INTERNATIONAL7 = 141,
            SCANCODE_INTERNATIONAL8 = 142,
            SCANCODE_INTERNATIONAL9 = 143,
            SCANCODE_LANG1 = 144,
            SCANCODE_LANG2 = 145,
            SCANCODE_LANG3 = 146,
            SCANCODE_LANG4 = 147,
            SCANCODE_LANG5 = 148,
            SCANCODE_LANG6 = 149,
            SCANCODE_LANG7 = 150,
            SCANCODE_LANG8 = 151,
            SCANCODE_LANG9 = 152,
            SCANCODE_ALTERASE = 153,
            SCANCODE_SYSREQ = 154,
            SCANCODE_CANCEL = 155,
            SCANCODE_CLEAR = 156,
            SCANCODE_PRIOR = 157,
            SCANCODE_RETURN2 = 158,
            SCANCODE_SEPARATOR = 159,
            SCANCODE_OUT = 160,
            SCANCODE_OPER = 161,
            SCANCODE_CLEARAGAIN = 162,
            SCANCODE_CRSEL = 163,
            SCANCODE_EXSEL = 164,
            SCANCODE_KP_00 = 176,
            SCANCODE_KP_000 = 177,
            SCANCODE_THOUSANDSSEPARATOR = 178,
            SCANCODE_DECIMALSEPARATOR = 179,
            SCANCODE_CURRENCYUNIT = 180,
            SCANCODE_CURRENCYSUBUNIT = 181,
            SCANCODE_KP_LEFTPAREN = 182,
            SCANCODE_KP_RIGHTPAREN = 183,
            SCANCODE_KP_LEFTBRACE = 184,
            SCANCODE_KP_RIGHTBRACE = 185,
            SCANCODE_KP_TAB = 186,
            SCANCODE_KP_BACKSPACE = 187,
            SCANCODE_KP_A = 188,
            SCANCODE_KP_B = 189,
            SCANCODE_KP_C = 190,
            SCANCODE_KP_D = 191,
            SCANCODE_KP_E = 192,
            SCANCODE_KP_F = 193,
            SCANCODE_KP_XOR = 194,
            SCANCODE_KP_POWER = 195,
            SCANCODE_KP_PERCENT = 196,
            SCANCODE_KP_LESS = 197,
            SCANCODE_KP_GREATER = 198,
            SCANCODE_KP_AMPERSAND = 199,
            SCANCODE_KP_DBLAMPERSAND = 200,
            SCANCODE_KP_VERTICALBAR = 201,
            SCANCODE_KP_DBLVERTICALBAR = 202,
            SCANCODE_KP_COLON = 203,
            SCANCODE_KP_HASH = 204,
            SCANCODE_KP_SPACE = 205,
            SCANCODE_KP_AT = 206,
            SCANCODE_KP_EXCLAM = 207,
            SCANCODE_KP_MEMSTORE = 208,
            SCANCODE_KP_MEMRECALL = 209,
            SCANCODE_KP_MEMCLEAR = 210,
            SCANCODE_KP_MEMADD = 211,
            SCANCODE_KP_MEMSUBTRACT = 212,
            SCANCODE_KP_MEMMULTIPLY = 213,
            SCANCODE_KP_MEMDIVIDE = 214,
            SCANCODE_KP_PLUSMINUS = 215,
            SCANCODE_KP_CLEAR = 216,
            SCANCODE_KP_CLEARENTRY = 217,
            SCANCODE_KP_BINARY = 218,
            SCANCODE_KP_OCTAL = 219,
            SCANCODE_KP_DECIMAL = 220,
            SCANCODE_KP_HEXADECIMAL = 221,
            SCANCODE_LCTRL = 224,
            SCANCODE_LSHIFT = 225,
            SCANCODE_LALT = 226,
            SCANCODE_LGUI = 227,
            SCANCODE_RCTRL = 228,
            SCANCODE_RSHIFT = 229,
            SCANCODE_RALT = 230,
            SCANCODE_RGUI = 231,
            SCANCODE_MODE = 257,
            SCANCODE_AUDIONEXT = 258,
            SCANCODE_AUDIOPREV = 259,
            SCANCODE_AUDIOSTOP = 260,
            SCANCODE_AUDIOPLAY = 261,
            SCANCODE_AUDIOMUTE = 262,
            SCANCODE_MEDIASELECT = 263,
            SCANCODE_WWW = 264,
            SCANCODE_MAIL = 265,
            SCANCODE_CALCULATOR = 266,
            SCANCODE_COMPUTER = 267,
            SCANCODE_AC_SEARCH = 268,
            SCANCODE_AC_HOME = 269,
            SCANCODE_AC_BACK = 270,
            SCANCODE_AC_FORWARD = 271,
            SCANCODE_AC_STOP = 272,
            SCANCODE_AC_REFRESH = 273,
            SCANCODE_AC_BOOKMARKS = 274,
            SCANCODE_BRIGHTNESSDOWN = 275,
            SCANCODE_BRIGHTNESSUP = 276,
            SCANCODE_DISPLAYSWITCH = 277,
            SCANCODE_KBDILLUMTOGGLE = 278,
            SCANCODE_KBDILLUMDOWN = 279,
            SCANCODE_KBDILLUMUP = 280,
            SCANCODE_EJECT = 281,
            SCANCODE_SLEEP = 282,
            SCANCODE_APP1 = 283,
            SCANCODE_APP2 = 284,
            SCANCODE_AUDIOREWIND = 285,
            SCANCODE_AUDIOFASTFORWARD = 286,
            NUM_SCANCODES = 0x200
        }

        public enum Keycode
        {
            SDLK_UNKNOWN = 0,
            SDLK_RETURN = 13,
            SDLK_ESCAPE = 27,
            SDLK_BACKSPACE = 8,
            SDLK_TAB = 9,
            SDLK_SPACE = 0x20,
            SDLK_EXCLAIM = 33,
            SDLK_QUOTEDBL = 34,
            SDLK_HASH = 35,
            SDLK_PERCENT = 37,
            SDLK_DOLLAR = 36,
            SDLK_AMPERSAND = 38,
            SDLK_QUOTE = 39,
            SDLK_LEFTPAREN = 40,
            SDLK_RIGHTPAREN = 41,
            SDLK_ASTERISK = 42,
            SDLK_PLUS = 43,
            SDLK_COMMA = 44,
            SDLK_MINUS = 45,
            SDLK_PERIOD = 46,
            SDLK_SLASH = 47,
            SDLK_0 = 48,
            SDLK_1 = 49,
            SDLK_2 = 50,
            SDLK_3 = 51,
            SDLK_4 = 52,
            SDLK_5 = 53,
            SDLK_6 = 54,
            SDLK_7 = 55,
            SDLK_8 = 56,
            SDLK_9 = 57,
            SDLK_COLON = 58,
            SDLK_SEMICOLON = 59,
            SDLK_LESS = 60,
            SDLK_EQUALS = 61,
            SDLK_GREATER = 62,
            SDLK_QUESTION = 0x3F,
            SDLK_AT = 0x40,
            SDLK_LEFTBRACKET = 91,
            SDLK_BACKSLASH = 92,
            SDLK_RIGHTBRACKET = 93,
            SDLK_CARET = 94,
            SDLK_UNDERSCORE = 95,
            SDLK_BACKQUOTE = 96,
            SDLK_a = 97,
            SDLK_b = 98,
            SDLK_c = 99,
            SDLK_d = 100,
            SDLK_e = 101,
            SDLK_f = 102,
            SDLK_g = 103,
            SDLK_h = 104,
            SDLK_i = 105,
            SDLK_j = 106,
            SDLK_k = 107,
            SDLK_l = 108,
            SDLK_m = 109,
            SDLK_n = 110,
            SDLK_o = 111,
            SDLK_p = 112,
            SDLK_q = 113,
            SDLK_r = 114,
            SDLK_s = 115,
            SDLK_t = 116,
            SDLK_u = 117,
            SDLK_v = 118,
            SDLK_w = 119,
            SDLK_x = 120,
            SDLK_y = 121,
            SDLK_z = 122,
            SDLK_CAPSLOCK = 1073741881,
            SDLK_F1 = 1073741882,
            SDLK_F2 = 1073741883,
            SDLK_F3 = 1073741884,
            SDLK_F4 = 1073741885,
            SDLK_F5 = 1073741886,
            SDLK_F6 = 1073741887,
            SDLK_F7 = 1073741888,
            SDLK_F8 = 1073741889,
            SDLK_F9 = 1073741890,
            SDLK_F10 = 1073741891,
            SDLK_F11 = 1073741892,
            SDLK_F12 = 1073741893,
            SDLK_PRINTSCREEN = 1073741894,
            SDLK_SCROLLLOCK = 1073741895,
            SDLK_PAUSE = 1073741896,
            SDLK_INSERT = 1073741897,
            SDLK_HOME = 1073741898,
            SDLK_PAGEUP = 1073741899,
            SDLK_DELETE = 0x7F,
            SDLK_END = 1073741901,
            SDLK_PAGEDOWN = 1073741902,
            SDLK_RIGHT = 1073741903,
            SDLK_LEFT = 1073741904,
            SDLK_DOWN = 1073741905,
            SDLK_UP = 1073741906,
            SDLK_NUMLOCKCLEAR = 1073741907,
            SDLK_KP_DIVIDE = 1073741908,
            SDLK_KP_MULTIPLY = 1073741909,
            SDLK_KP_MINUS = 1073741910,
            SDLK_KP_PLUS = 1073741911,
            SDLK_KP_ENTER = 1073741912,
            SDLK_KP_1 = 1073741913,
            SDLK_KP_2 = 1073741914,
            SDLK_KP_3 = 1073741915,
            SDLK_KP_4 = 1073741916,
            SDLK_KP_5 = 1073741917,
            SDLK_KP_6 = 1073741918,
            SDLK_KP_7 = 1073741919,
            SDLK_KP_8 = 1073741920,
            SDLK_KP_9 = 1073741921,
            SDLK_KP_0 = 1073741922,
            SDLK_KP_PERIOD = 1073741923,
            SDLK_APPLICATION = 1073741925,
            SDLK_POWER = 1073741926,
            SDLK_KP_EQUALS = 1073741927,
            SDLK_F13 = 1073741928,
            SDLK_F14 = 1073741929,
            SDLK_F15 = 1073741930,
            SDLK_F16 = 1073741931,
            SDLK_F17 = 1073741932,
            SDLK_F18 = 1073741933,
            SDLK_F19 = 1073741934,
            SDLK_F20 = 1073741935,
            SDLK_F21 = 1073741936,
            SDLK_F22 = 1073741937,
            SDLK_F23 = 1073741938,
            SDLK_F24 = 1073741939,
            SDLK_EXECUTE = 1073741940,
            SDLK_HELP = 1073741941,
            SDLK_MENU = 1073741942,
            SDLK_SELECT = 1073741943,
            SDLK_STOP = 1073741944,
            SDLK_AGAIN = 1073741945,
            SDLK_UNDO = 1073741946,
            SDLK_CUT = 1073741947,
            SDLK_COPY = 1073741948,
            SDLK_PASTE = 1073741949,
            SDLK_FIND = 1073741950,
            SDLK_MUTE = 1073741951,
            SDLK_VOLUMEUP = 1073741952,
            SDLK_VOLUMEDOWN = 1073741953,
            SDLK_KP_COMMA = 1073741957,
            SDLK_KP_EQUALSAS400 = 1073741958,
            SDLK_ALTERASE = 1073741977,
            SDLK_SYSREQ = 1073741978,
            SDLK_CANCEL = 1073741979,
            SDLK_CLEAR = 1073741980,
            SDLK_PRIOR = 1073741981,
            SDLK_RETURN2 = 1073741982,
            SDLK_SEPARATOR = 1073741983,
            SDLK_OUT = 1073741984,
            SDLK_OPER = 1073741985,
            SDLK_CLEARAGAIN = 1073741986,
            SDLK_CRSEL = 1073741987,
            SDLK_EXSEL = 1073741988,
            SDLK_KP_00 = 1073742000,
            SDLK_KP_000 = 1073742001,
            SDLK_THOUSANDSSEPARATOR = 1073742002,
            SDLK_DECIMALSEPARATOR = 1073742003,
            SDLK_CURRENCYUNIT = 1073742004,
            SDLK_CURRENCYSUBUNIT = 1073742005,
            SDLK_KP_LEFTPAREN = 1073742006,
            SDLK_KP_RIGHTPAREN = 1073742007,
            SDLK_KP_LEFTBRACE = 1073742008,
            SDLK_KP_RIGHTBRACE = 1073742009,
            SDLK_KP_TAB = 1073742010,
            SDLK_KP_BACKSPACE = 1073742011,
            SDLK_KP_A = 1073742012,
            SDLK_KP_B = 1073742013,
            SDLK_KP_C = 1073742014,
            SDLK_KP_D = 1073742015,
            SDLK_KP_E = 1073742016,
            SDLK_KP_F = 1073742017,
            SDLK_KP_XOR = 1073742018,
            SDLK_KP_POWER = 1073742019,
            SDLK_KP_PERCENT = 1073742020,
            SDLK_KP_LESS = 1073742021,
            SDLK_KP_GREATER = 1073742022,
            SDLK_KP_AMPERSAND = 1073742023,
            SDLK_KP_DBLAMPERSAND = 1073742024,
            SDLK_KP_VERTICALBAR = 1073742025,
            SDLK_KP_DBLVERTICALBAR = 1073742026,
            SDLK_KP_COLON = 1073742027,
            SDLK_KP_HASH = 1073742028,
            SDLK_KP_SPACE = 1073742029,
            SDLK_KP_AT = 1073742030,
            SDLK_KP_EXCLAM = 1073742031,
            SDLK_KP_MEMSTORE = 1073742032,
            SDLK_KP_MEMRECALL = 1073742033,
            SDLK_KP_MEMCLEAR = 1073742034,
            SDLK_KP_MEMADD = 1073742035,
            SDLK_KP_MEMSUBTRACT = 1073742036,
            SDLK_KP_MEMMULTIPLY = 1073742037,
            SDLK_KP_MEMDIVIDE = 1073742038,
            SDLK_KP_PLUSMINUS = 1073742039,
            SDLK_KP_CLEAR = 1073742040,
            SDLK_KP_CLEARENTRY = 1073742041,
            SDLK_KP_BINARY = 1073742042,
            SDLK_KP_OCTAL = 1073742043,
            SDLK_KP_DECIMAL = 1073742044,
            SDLK_KP_HEXADECIMAL = 1073742045,
            SDLK_LCTRL = 1073742048,
            SDLK_LSHIFT = 1073742049,
            SDLK_LALT = 1073742050,
            SDLK_LGUI = 1073742051,
            SDLK_RCTRL = 1073742052,
            SDLK_RSHIFT = 1073742053,
            SDLK_RALT = 1073742054,
            SDLK_RGUI = 1073742055,
            SDLK_MODE = 1073742081,
            SDLK_AUDIONEXT = 1073742082,
            SDLK_AUDIOPREV = 1073742083,
            SDLK_AUDIOSTOP = 1073742084,
            SDLK_AUDIOPLAY = 1073742085,
            SDLK_AUDIOMUTE = 1073742086,
            SDLK_MEDIASELECT = 1073742087,
            SDLK_WWW = 1073742088,
            SDLK_MAIL = 1073742089,
            SDLK_CALCULATOR = 1073742090,
            SDLK_COMPUTER = 1073742091,
            SDLK_AC_SEARCH = 1073742092,
            SDLK_AC_HOME = 1073742093,
            SDLK_AC_BACK = 1073742094,
            SDLK_AC_FORWARD = 1073742095,
            SDLK_AC_STOP = 1073742096,
            SDLK_AC_REFRESH = 1073742097,
            SDLK_AC_BOOKMARKS = 1073742098,
            SDLK_BRIGHTNESSDOWN = 1073742099,
            SDLK_BRIGHTNESSUP = 1073742100,
            SDLK_DISPLAYSWITCH = 1073742101,
            SDLK_KBDILLUMTOGGLE = 1073742102,
            SDLK_KBDILLUMDOWN = 1073742103,
            SDLK_KBDILLUMUP = 1073742104,
            SDLK_EJECT = 1073742105,
            SDLK_SLEEP = 1073742106,
            SDLK_APP1 = 1073742107,
            SDLK_APP2 = 1073742108,
            SDLK_AUDIOREWIND = 1073742109,
            SDLK_AUDIOFASTFORWARD = 1073742110
        }

        [Flags]
        public enum Keymod : ushort
        {
            KMOD_NONE = 0x0,
            KMOD_LSHIFT = 0x1,
            KMOD_RSHIFT = 0x2,
            KMOD_LCTRL = 0x40,
            KMOD_RCTRL = 0x80,
            KMOD_LALT = 0x100,
            KMOD_RALT = 0x200,
            KMOD_LGUI = 0x400,
            KMOD_RGUI = 0x800,
            KMOD_NUM = 0x1000,
            KMOD_CAPS = 0x2000,
            KMOD_MODE = 0x4000,
            KMOD_RESERVED = 0x8000,
            KMOD_CTRL = 0xC0,
            KMOD_SHIFT = 0x3,
            KMOD_ALT = 0x300,
            KMOD_GUI = 0xC00
        }

        public struct Keysym
        {
            public Scancode scancode;

            public Keycode sym;

            public Keymod mod;

            public uint unicode;
        }

        public enum SystemCursor
        {
            SYSTEM_CURSOR_ARROW,
            SYSTEM_CURSOR_IBEAM,
            SYSTEM_CURSOR_WAIT,
            SYSTEM_CURSOR_CROSSHAIR,
            SYSTEM_CURSOR_WAITARROW,
            SYSTEM_CURSOR_SIZENWSE,
            SYSTEM_CURSOR_SIZENESW,
            SYSTEM_CURSOR_SIZEWE,
            SYSTEM_CURSOR_SIZENS,
            SYSTEM_CURSOR_SIZEALL,
            SYSTEM_CURSOR_NO,
            SYSTEM_CURSOR_HAND,
            NUM_SYSTEM_CURSORS
        }

        public struct Finger
        {
            public long id;

            public float x;

            public float y;

            public float pressure;
        }

        public enum TouchDeviceType
        {
            TOUCH_DEVICE_INVALID = -1,
            TOUCH_DEVICE_DIRECT,
            TOUCH_DEVICE_INDIRECT_ABSOLUTE,
            TOUCH_DEVICE_INDIRECT_RELATIVE
        }

        public enum JoystickPowerLevel
        {
            JOYSTICK_POWER_UNKNOWN = -1,
            JOYSTICK_POWER_EMPTY,
            JOYSTICK_POWER_LOW,
            JOYSTICK_POWER_MEDIUM,
            JOYSTICK_POWER_FULL,
            JOYSTICK_POWER_WIRED,
            JOYSTICK_POWER_MAX
        }

        public enum JoystickType
        {
            JOYSTICK_TYPE_UNKNOWN,
            JOYSTICK_TYPE_GAMECONTROLLER,
            JOYSTICK_TYPE_WHEEL,
            JOYSTICK_TYPE_ARCADE_STICK,
            JOYSTICK_TYPE_FLIGHT_STICK,
            JOYSTICK_TYPE_DANCE_PAD,
            JOYSTICK_TYPE_GUITAR,
            JOYSTICK_TYPE_DRUM_KIT,
            JOYSTICK_TYPE_ARCADE_PAD
        }

        public enum GameControllerBindType
        {
            CONTROLLER_BINDTYPE_NONE,
            CONTROLLER_BINDTYPE_BUTTON,
            CONTROLLER_BINDTYPE_AXIS,
            CONTROLLER_BINDTYPE_HAT
        }

        public enum GameControllerAxis
        {
            CONTROLLER_AXIS_INVALID = -1,
            CONTROLLER_AXIS_LEFTX,
            CONTROLLER_AXIS_LEFTY,
            CONTROLLER_AXIS_RIGHTX,
            CONTROLLER_AXIS_RIGHTY,
            CONTROLLER_AXIS_TRIGGERLEFT,
            CONTROLLER_AXIS_TRIGGERRIGHT,
            CONTROLLER_AXIS_MAX
        }

        public enum GameControllerButton
        {
            CONTROLLER_BUTTON_INVALID = -1,
            CONTROLLER_BUTTON_A,
            CONTROLLER_BUTTON_B,
            CONTROLLER_BUTTON_X,
            CONTROLLER_BUTTON_Y,
            CONTROLLER_BUTTON_BACK,
            CONTROLLER_BUTTON_GUIDE,
            CONTROLLER_BUTTON_START,
            CONTROLLER_BUTTON_LEFTSTICK,
            CONTROLLER_BUTTON_RIGHTSTICK,
            CONTROLLER_BUTTON_LEFTSHOULDER,
            CONTROLLER_BUTTON_RIGHTSHOULDER,
            CONTROLLER_BUTTON_DPAD_UP,
            CONTROLLER_BUTTON_DPAD_DOWN,
            CONTROLLER_BUTTON_DPAD_LEFT,
            CONTROLLER_BUTTON_DPAD_RIGHT,
            CONTROLLER_BUTTON_MISC1,
            CONTROLLER_BUTTON_PADDLE1,
            CONTROLLER_BUTTON_PADDLE2,
            CONTROLLER_BUTTON_PADDLE3,
            CONTROLLER_BUTTON_PADDLE4,
            CONTROLLER_BUTTON_TOUCHPAD,
            CONTROLLER_BUTTON_MAX
        }

        public enum GameControllerType
        {
            CONTROLLER_TYPE_UNKNOWN,
            CONTROLLER_TYPE_XBOX360,
            CONTROLLER_TYPE_XBOXONE,
            CONTROLLER_TYPE_PS3,
            CONTROLLER_TYPE_PS4,
            CONTROLLER_TYPE_NINTENDO_SWITCH_PRO,
            CONTROLLER_TYPE_VIRTUAL,
            CONTROLLER_TYPE_PS5
        }

        public struct INTERNAL_GameControllerButtonBind_hat
        {
            public int hat;

            public int hat_mask;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNAL_GameControllerButtonBind_union
        {
            [FieldOffset(0)]
            public int button;

            [FieldOffset(0)]
            public int axis;

            [FieldOffset(0)]
            public INTERNAL_GameControllerButtonBind_hat hat;
        }

        public struct GameControllerButtonBind
        {
            public GameControllerBindType bindType;

            public INTERNAL_GameControllerButtonBind_union value;
        }

        private struct INTERNAL_GameControllerButtonBind
        {
            public int bindType;

            public int unionVal0;

            public int unionVal1;
        }

        public struct HapticDirection
        {
            public byte type;

            public unsafe fixed int dir[3];
        }

        public struct HapticConstant
        {
            public ushort type;

            public HapticDirection direction;

            public uint length;

            public ushort delay;

            public ushort button;

            public ushort interval;

            public short level;

            public ushort attack_length;

            public ushort attack_level;

            public ushort fade_length;

            public ushort fade_level;
        }

        public struct HapticPeriodic
        {
            public ushort type;

            public HapticDirection direction;

            public uint length;

            public ushort delay;

            public ushort button;

            public ushort interval;

            public ushort period;

            public short magnitude;

            public short offset;

            public ushort phase;

            public ushort attack_length;

            public ushort attack_level;

            public ushort fade_length;

            public ushort fade_level;
        }

        public struct HapticCondition
        {
            public ushort type;

            public HapticDirection direction;

            public uint length;

            public ushort delay;

            public ushort button;

            public ushort interval;

            public unsafe fixed ushort right_sat[3];

            public unsafe fixed ushort left_sat[3];

            public unsafe fixed short right_coeff[3];

            public unsafe fixed short left_coeff[3];

            public unsafe fixed ushort deadband[3];

            public unsafe fixed short center[3];
        }

        public struct HapticRamp
        {
            public ushort type;

            public HapticDirection direction;

            public uint length;

            public ushort delay;

            public ushort button;

            public ushort interval;

            public short start;

            public short end;

            public ushort attack_length;

            public ushort attack_level;

            public ushort fade_length;

            public ushort fade_level;
        }

        public struct HapticLeftRight
        {
            public ushort type;

            public uint length;

            public ushort large_magnitude;

            public ushort small_magnitude;
        }

        public struct HapticCustom
        {
            public ushort type;

            public HapticDirection direction;

            public uint length;

            public ushort delay;

            public ushort button;

            public ushort interval;

            public byte channels;

            public ushort period;

            public ushort samples;

            public IntPtr data;

            public ushort attack_length;

            public ushort attack_level;

            public ushort fade_length;

            public ushort fade_level;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct HapticEffect
        {
            [FieldOffset(0)]
            public ushort type;

            [FieldOffset(0)]
            public HapticConstant constant;

            [FieldOffset(0)]
            public HapticPeriodic periodic;

            [FieldOffset(0)]
            public HapticCondition condition;

            [FieldOffset(0)]
            public HapticRamp ramp;

            [FieldOffset(0)]
            public HapticLeftRight leftright;

            [FieldOffset(0)]
            public HapticCustom custom;
        }

        public enum SensorType
        {
            SENSOR_INVALID = -1,
            SENSOR_UNKNOWN,
            SENSOR_ACCEL,
            SENSOR_GYRO
        }

        public enum AudioStatus
        {
            AUDIO_STOPPED,
            AUDIO_PLAYING,
            AUDIO_PAUSED
        }

        public enum AudioFormat : ushort
        {
            /// <summary>
            /// < Unsigned 8-bit samples
            /// </summary>
            U8 = 0x0008,
            /// <summary>
            /// < Signed 8-bit samples
            /// </summary>
            S8 = 0x8008,
            /// <summary>
            /// < Unsigned 16-bit samples
            /// </summary>
            U16LSB = 0x0010,
            /// <summary>
            /// < Signed 16-bit samples
            /// </summary>
            S16LSB = 0x8010,
            /// <summary>
            /// < As above, but big-endian byte order
            /// </summary>
            U16MSB = 0x1010,
            /// <summary>
            /// < As above, but big-endian byte order
            /// </summary>
            S16MSB = 0x9010,
            U16 = U16LSB,
            S16 = S16LSB,
            /// <summary>
            /// < 32-bit integer samples
            /// </summary>
            S32LSB = 0x8020,
            /// <summary>
            /// < As above, but big-endian byte order
            /// </summary>
            S32MSB = 0x9020,
            S32 = S32LSB,
            /// <summary>
            /// < 32-bit floating point samples
            /// </summary>
            F32LSB = 0x8120,
            /// <summary>
            /// < As above, but big-endian byte order
            /// </summary>
            F32MSB = 0x9120,
            F32 = F32LSB,

            /*
            #if BYTEORDER == LIL_ENDIAN
            #define AUDIO_U16SYS    AUDIO_U16LSB
            #define AUDIO_S16SYS    AUDIO_S16LSB
            #define AUDIO_S32SYS    AUDIO_S32LSB
            #define AUDIO_F32SYS    AUDIO_F32LSB
            #else
            #define AUDIO_U16SYS    AUDIO_U16MSB
            #define AUDIO_S16SYS    AUDIO_S16MSB
            #define AUDIO_S32SYS    AUDIO_S32MSB
            #define AUDIO_F32SYS    AUDIO_F32MSB
            #endif
            */
        }

        /// <summary>
        /// A structure that contains the audio output format. It also contains a callback that is called when the audio device needs more data.
        /// </summary>
        /// <remarks>
        /// This structure is used by <see cref="OpenAudioDevice"/> and <see cref="LoadWAV"/>. While all fields are used by <see cref="OpenAudioDevice"/>, only freq, format, channels, and samples are used by <see cref="LoadWAV"/>.
        /// <list type="table">
        /// <listheader>
        /// <term>Field</term>
        /// <description>Description</description>
        /// </listheader>
        /// <item>
        /// <term>freq</term>
        /// <description>specifies the number of sample frames sent to the sound device per second. The Nyquist Theorem states that the audio sampling frequency must be exactly twice the highest frequency represented in the audio. Humans can hear up to slightly under 20kHz, declining to 16kHz or lower as we age. Standard CD quality audio uses 44100. DVDs and the Opus audio codec use 48000. Values higher than 48000 generally should not be used for playback purposes because they use more memory, use more CPU, and can cause other problems as explained in this blog post by Chris Montgomery of Xiph.</description>
        /// </item>
        /// <item>
        /// <term>channels</term>
        /// <description>specifies the number of output channels. As of SDL 2.0, supported values are 1 (mono), 2 (stereo), 4 (quad), and 6 (5.1).</description>
        /// </item>
        /// <item>
        /// <term>samples</term>
        /// <description>
        /// <para>specifies a unit of audio data. When used with <see cref="OpenAudioDevice"/> this refers to the size of the audio buffer in sample frames. A sample frame is a chunk of audio data of the size specified in format multiplied by the number of channels. When the <see cref="AudioSpec "/> used with <see cref="LoadWAV"/> samples is set to 4096. This field's value must be a power of two.</para>
        /// <para>The values silence and size are calculated by <see cref="OpenAudioDevice"/>.</para>
        /// <para>Channel data is interleaved.Stereo samples are stored in left/right ordering.Quad is stored in front-left/front-right/rear-left/rear-right order. 5.1 is stored in front-left/front-right/center/low-freq/rear-left/rear-right ordering ("low-freq" is the ".1" speaker).</para>
        /// <para>The function prototype for callback is: <c>void SDL_AudioCallback(void* userdata, Uint8* stream, int len)</c></para>
        /// <para>
        /// <list type="table">
        /// <item>
        /// <term>userdata</term>
        /// <description>an application-specific parameter saved in the <see cref="AudioSpec"/> structure's userdata field</description>
        /// </item>
        /// <item>
        /// <term>stream</term>
        /// <description>a pointer to the audio data buffer filled in by <see cref="AudioCallback"/></description>
        /// </item>
        /// <item>
        /// <term>len</term>
        /// <description>the length of that buffer in bytes</description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>Once the callback returns, the buffer will no longer be valid. Stereo samples are stored in a LRLRLR ordering.</para>
        /// <para>The callback must completely initialize the buffer; as of SDL 2.0, this buffer is not initialized before the callback is called. If there is nothing to play, the callback should fill the buffer with silence.</para>
        /// <para>With SDL >= 2.0.4 you can choose to avoid callbacks and use <see cref="QueueAudio"/> instead, if you like. Just open your audio device with a NULL callback.</para>
        /// </description>
        /// </item>
        /// </list>
        /// </remarks>
        public struct AudioSpec
        {
            /// <summary>
            /// DSP frequency (samples per second); see Remarks for details
            /// </summary>
            public int freq;
            /// <summary>
            /// audio data format; see Remarks for details
            /// </summary>
            [MarshalAs(UnmanagedType.U2)]
            public AudioFormat format;
            /// <summary>
            /// number of separate sound channels: see Remarks for details
            /// </summary>
            public byte channels;
            /// <summary>
            /// audio buffer silence value (calculated)
            /// </summary>
            public byte silence;
            /// <summary>
            /// audio buffer size in samples (power of 2); see Remarks for details
            /// </summary>
            public ushort samples;
            /// <summary>
            /// audio buffer size in bytes (calculated)
            /// </summary>
            public uint size;
            /// <summary>
            /// the function to call when the audio device needs more data; see Remarks for details
            /// </summary>
            public delegate* unmanaged[Cdecl]<void*, byte*, int, void> callback;
            /// <summary>
            /// a pointer that is passed to callback(otherwise ignored by SDL)
            /// </summary>
            public IntPtr userdata;
        }

        // typedef void (SDLCALL * SDL_AudioCallback) (void *userdata, Uint8 * stream, int len);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate uint TimerCallback(uint interval, IntPtr param);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate IntPtr WindowsMessageHook(IntPtr userdata, IntPtr hWnd, uint message, ulong wParam, long lParam);

        [UnmanagedFunctionPointer(CALLING_CONVENTION)]
        public delegate void iPhoneAnimationCallback(IntPtr p);

        public enum WinRT_DeviceFamily
        {
            WINRT_DEVICEFAMILY_UNKNOWN,
            WINRT_DEVICEFAMILY_DESKTOP,
            WINRT_DEVICEFAMILY_MOBILE,
            WINRT_DEVICEFAMILY_XBOX
        }

        public enum SYSWM_TYPE
        {
            SYSWM_UNKNOWN,
            SYSWM_WINDOWS,
            SYSWM_X11,
            SYSWM_DIRECTFB,
            SYSWM_COCOA,
            SYSWM_UIKIT,
            SYSWM_WAYLAND,
            SYSWM_MIR,
            SYSWM_WINRT,
            SYSWM_ANDROID,
            SYSWM_VIVANTE,
            SYSWM_OS2,
            SYSWM_HAIKU
        }

        public struct INTERNAL_windows_wminfo
        {
            public IntPtr window;

            public IntPtr hdc;

            public IntPtr hinstance;
        }

        public struct INTERNAL_winrt_wminfo
        {
            public IntPtr window;
        }

        public struct INTERNAL_x11_wminfo
        {
            public IntPtr display;

            public IntPtr window;
        }

        public struct INTERNAL_directfb_wminfo
        {
            public IntPtr dfb;

            public IntPtr window;

            public IntPtr surface;
        }

        public struct INTERNAL_cocoa_wminfo
        {
            public IntPtr window;
        }

        public struct INTERNAL_uikit_wminfo
        {
            public IntPtr window;

            public uint framebuffer;

            public uint colorbuffer;

            public uint resolveFramebuffer;
        }

        public struct INTERNAL_wayland_wminfo
        {
            public IntPtr display;

            public IntPtr surface;

            public IntPtr shell_surface;
        }

        public struct INTERNAL_mir_wminfo
        {
            public IntPtr connection;

            public IntPtr surface;
        }

        public struct INTERNAL_android_wminfo
        {
            public IntPtr window;

            public IntPtr surface;
        }

        public struct INTERNAL_vivante_wminfo
        {
            public IntPtr display;

            public IntPtr window;
        }

        public struct INTERNAL_os2_wminfo
        {
            public IntPtr hwnd;

            public IntPtr hwndFrame;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct INTERNAL_SysWMDriverUnion
        {
            [FieldOffset(0)]
            public INTERNAL_windows_wminfo win;

            [FieldOffset(0)]
            public INTERNAL_winrt_wminfo winrt;

            [FieldOffset(0)]
            public INTERNAL_x11_wminfo x11;

            [FieldOffset(0)]
            public INTERNAL_directfb_wminfo dfb;

            [FieldOffset(0)]
            public INTERNAL_cocoa_wminfo cocoa;

            [FieldOffset(0)]
            public INTERNAL_uikit_wminfo uikit;

            [FieldOffset(0)]
            public INTERNAL_wayland_wminfo wl;

            [FieldOffset(0)]
            public INTERNAL_mir_wminfo mir;

            [FieldOffset(0)]
            public INTERNAL_android_wminfo android;

            [FieldOffset(0)]
            public INTERNAL_vivante_wminfo vivante;

            [FieldOffset(0)]
            public INTERNAL_os2_wminfo os2;
        }

        public struct SysWMinfo
        {
            public version version;

            public SYSWM_TYPE subsystem;

            public INTERNAL_SysWMDriverUnion info;
        }

        public enum PowerState
        {
            POWERSTATE_UNKNOWN,
            POWERSTATE_ON_BATTERY,
            POWERSTATE_NO_BATTERY,
            POWERSTATE_CHARGING,
            POWERSTATE_CHARGED
        }

        public struct Locale
        {
            private IntPtr language;

            private IntPtr country;
        }

        public const int RW_SEEK_SET = 0;

        public const int RW_SEEK_CUR = 1;

        public const int RW_SEEK_END = 2;

        public const uint RWOPS_UNKNOWN = 0u;

        public const uint RWOPS_WINFILE = 1u;

        public const uint RWOPS_STDFILE = 2u;

        public const uint RWOPS_JNIFILE = 3u;

        public const uint RWOPS_MEMORY = 4u;

        public const uint RWOPS_MEMORY_RO = 5u;

        [Flags]
        public enum InitFlags : uint
        {
            TIMER = 0x00000001u,
            AUDIO = 0x00000010u,
            /// <summary>
            /// SDL_INIT_VIDEO implies SDL_INIT_EVENTS
            /// </summary>
            VIDEO = 0x00000020u,
            /// <summary>
            /// SDL_INIT_JOYSTICK implies SDL_INIT_EVENTS
            /// </summary>
            JOYSTICK = 0x00000200u,
            HAPTIC = 0x00001000u,
            /// <summary>
            /// SDL_INIT_GAMECONTROLLER implies SDL_INIT_JOYSTICK
            /// </summary>
            GAMECONTROLLER = 0x00002000u,
            EVENTS = 0x00004000u,
            /// <summary>
            /// compatibility; this flag is ignored.
            /// </summary>
            NOPARACHUTE = 0x00100000u,
            EVERYTHING = TIMER | AUDIO | VIDEO | EVENTS | JOYSTICK | HAPTIC | GAMECONTROLLER
        }

        public const string HINT_FRAMEBUFFER_ACCELERATION = "FRAMEBUFFER_ACCELERATION";

        public const string HINT_RENDER_DRIVER = "RENDER_DRIVER";

        public const string HINT_RENDER_OPENGL_SHADERS = "RENDER_OPENGL_SHADERS";

        public const string HINT_RENDER_DIRECT3D_THREADSAFE = "RENDER_DIRECT3D_THREADSAFE";

        public const string HINT_RENDER_VSYNC = "RENDER_VSYNC";

        public const string HINT_VIDEO_X11_XVIDMODE = "VIDEO_X11_XVIDMODE";

        public const string HINT_VIDEO_X11_XINERAMA = "VIDEO_X11_XINERAMA";

        public const string HINT_VIDEO_X11_XRANDR = "VIDEO_X11_XRANDR";

        public const string HINT_GRAB_KEYBOARD = "GRAB_KEYBOARD";

        public const string HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS = "VIDEO_MINIMIZE_ON_FOCUS_LOSS";

        public const string HINT_IDLE_TIMER_DISABLED = "IOS_IDLE_TIMER_DISABLED";

        public const string HINT_ORIENTATIONS = "IOS_ORIENTATIONS";

        public const string HINT_XINPUT_ENABLED = "XINPUT_ENABLED";

        public const string HINT_GAMECONTROLLERCONFIG = "GAMECONTROLLERCONFIG";

        public const string HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS = "JOYSTICK_ALLOW_BACKGROUND_EVENTS";

        public const string HINT_ALLOW_TOPMOST = "ALLOW_TOPMOST";

        public const string HINT_TIMER_RESOLUTION = "TIMER_RESOLUTION";

        public const string HINT_RENDER_SCALE_QUALITY = "RENDER_SCALE_QUALITY";

        public const string HINT_VIDEO_HIGHDPI_DISABLED = "VIDEO_HIGHDPI_DISABLED";

        public const string HINT_CTRL_CLICK_EMULATE_RIGHT_CLICK = "CTRL_CLICK_EMULATE_RIGHT_CLICK";

        public const string HINT_VIDEO_WIN_D3DCOMPILER = "VIDEO_WIN_D3DCOMPILER";

        public const string HINT_MOUSE_RELATIVE_MODE_WARP = "MOUSE_RELATIVE_MODE_WARP";

        public const string HINT_VIDEO_WINDOW_SHARE_PIXEL_FORMAT = "VIDEO_WINDOW_SHARE_PIXEL_FORMAT";

        public const string HINT_VIDEO_ALLOW_SCREENSAVER = "VIDEO_ALLOW_SCREENSAVER";

        public const string HINT_ACCELEROMETER_AS_JOYSTICK = "ACCELEROMETER_AS_JOYSTICK";

        public const string HINT_VIDEO_MAC_FULLSCREEN_SPACES = "VIDEO_MAC_FULLSCREEN_SPACES";

        public const string HINT_WINRT_PRIVACY_POLICY_URL = "WINRT_PRIVACY_POLICY_URL";

        public const string HINT_WINRT_PRIVACY_POLICY_LABEL = "WINRT_PRIVACY_POLICY_LABEL";

        public const string HINT_WINRT_HANDLE_BACK_BUTTON = "WINRT_HANDLE_BACK_BUTTON";

        public const string HINT_NO_SIGNAL_HANDLERS = "NO_SIGNAL_HANDLERS";

        public const string HINT_IME_INTERNAL_EDITING = "IME_INTERNAL_EDITING";

        public const string HINT_ANDROID_SEPARATE_MOUSE_AND_TOUCH = "ANDROID_SEPARATE_MOUSE_AND_TOUCH";

        public const string HINT_EMSCRIPTEN_KEYBOARD_ELEMENT = "EMSCRIPTEN_KEYBOARD_ELEMENT";

        public const string HINT_THREAD_STACK_SIZE = "THREAD_STACK_SIZE";

        public const string HINT_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN = "WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

        public const string HINT_WINDOWS_ENABLE_MESSAGELOOP = "WINDOWS_ENABLE_MESSAGELOOP";

        public const string HINT_WINDOWS_NO_CLOSE_ON_ALT_F4 = "WINDOWS_NO_CLOSE_ON_ALT_F4";

        public const string HINT_XINPUT_USE_OLD_JOYSTICK_MAPPING = "XINPUT_USE_OLD_JOYSTICK_MAPPING";

        public const string HINT_MAC_BACKGROUND_APP = "MAC_BACKGROUND_APP";

        public const string HINT_VIDEO_X11_NET_WM_PING = "VIDEO_X11_NET_WM_PING";

        public const string HINT_ANDROID_APK_EXPANSION_MAIN_FILE_VERSION = "ANDROID_APK_EXPANSION_MAIN_FILE_VERSION";

        public const string HINT_ANDROID_APK_EXPANSION_PATCH_FILE_VERSION = "ANDROID_APK_EXPANSION_PATCH_FILE_VERSION";

        public const string HINT_MOUSE_FOCUS_CLICKTHROUGH = "MOUSE_FOCUS_CLICKTHROUGH";

        public const string HINT_BMP_SAVE_LEGACY_FORMAT = "BMP_SAVE_LEGACY_FORMAT";

        public const string HINT_WINDOWS_DISABLE_THREAD_NAMING = "WINDOWS_DISABLE_THREAD_NAMING";

        public const string HINT_APPLE_TV_REMOTE_ALLOW_ROTATION = "APPLE_TV_REMOTE_ALLOW_ROTATION";

        public const string HINT_AUDIO_RESAMPLING_MODE = "AUDIO_RESAMPLING_MODE";

        public const string HINT_RENDER_LOGICAL_SIZE_MODE = "RENDER_LOGICAL_SIZE_MODE";

        public const string HINT_MOUSE_NORMAL_SPEED_SCALE = "MOUSE_NORMAL_SPEED_SCALE";

        public const string HINT_MOUSE_RELATIVE_SPEED_SCALE = "MOUSE_RELATIVE_SPEED_SCALE";

        public const string HINT_TOUCH_MOUSE_EVENTS = "TOUCH_MOUSE_EVENTS";

        public const string HINT_WINDOWS_INTRESOURCE_ICON = "WINDOWS_INTRESOURCE_ICON";

        public const string HINT_WINDOWS_INTRESOURCE_ICON_SMALL = "WINDOWS_INTRESOURCE_ICON_SMALL";

        public const string HINT_IOS_HIDE_HOME_INDICATOR = "IOS_HIDE_HOME_INDICATOR";

        public const string HINT_TV_REMOTE_AS_JOYSTICK = "TV_REMOTE_AS_JOYSTICK";

        public const string VIDEO_X11_NET_WM_BYPASS_COMPOSITOR = "VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

        public const string HINT_MOUSE_DOUBLE_CLICK_TIME = "MOUSE_DOUBLE_CLICK_TIME";

        public const string HINT_MOUSE_DOUBLE_CLICK_RADIUS = "MOUSE_DOUBLE_CLICK_RADIUS";

        public const string HINT_JOYSTICK_HIDAPI = "JOYSTICK_HIDAPI";

        public const string HINT_JOYSTICK_HIDAPI_PS4 = "JOYSTICK_HIDAPI_PS4";

        public const string HINT_JOYSTICK_HIDAPI_PS4_RUMBLE = "JOYSTICK_HIDAPI_PS4_RUMBLE";

        public const string HINT_JOYSTICK_HIDAPI_STEAM = "JOYSTICK_HIDAPI_STEAM";

        public const string HINT_JOYSTICK_HIDAPI_SWITCH = "JOYSTICK_HIDAPI_SWITCH";

        public const string HINT_JOYSTICK_HIDAPI_XBOX = "JOYSTICK_HIDAPI_XBOX";

        public const string HINT_ENABLE_STEAM_CONTROLLERS = "ENABLE_STEAM_CONTROLLERS";

        public const string HINT_ANDROID_TRAP_BACK_BUTTON = "ANDROID_TRAP_BACK_BUTTON";

        public const string HINT_MOUSE_TOUCH_EVENTS = "MOUSE_TOUCH_EVENTS";

        public const string HINT_GAMECONTROLLERCONFIG_FILE = "GAMECONTROLLERCONFIG_FILE";

        public const string HINT_ANDROID_BLOCK_ON_PAUSE = "ANDROID_BLOCK_ON_PAUSE";

        public const string HINT_RENDER_BATCHING = "RENDER_BATCHING";

        public const string HINT_EVENT_LOGGING = "EVENT_LOGGING";

        public const string HINT_WAVE_RIFF_CHUNK_SIZE = "WAVE_RIFF_CHUNK_SIZE";

        public const string HINT_WAVE_TRUNCATION = "WAVE_TRUNCATION";

        public const string HINT_WAVE_FACT_CHUNK = "WAVE_FACT_CHUNK";

        public const string HINT_VIDO_X11_WINDOW_VISUALID = "VIDEO_X11_WINDOW_VISUALID";

        public const string HINT_GAMECONTROLLER_USE_BUTTON_LABELS = "GAMECONTROLLER_USE_BUTTON_LABELS";

        public const string HINT_VIDEO_EXTERNAL_CONTEXT = "VIDEO_EXTERNAL_CONTEXT";

        public const string HINT_JOYSTICK_HIDAPI_GAMECUBE = "JOYSTICK_HIDAPI_GAMECUBE";

        public const string HINT_DISPLAY_USABLE_BOUNDS = "DISPLAY_USABLE_BOUNDS";

        public const string HINT_VIDEO_X11_FORCE_EGL = "VIDEO_X11_FORCE_EGL";

        public const string HINT_GAMECONTROLLERTYPE = "GAMECONTROLLERTYPE";

        public const string HINT_JOYSTICK_HIDAPI_CORRELATE_XINPUT = "JOYSTICK_HIDAPI_CORRELATE_XINPUT";

        public const string HINT_JOYSTICK_RAWINPUT = "JOYSTICK_RAWINPUT";

        public const string HINT_AUDIO_DEVICE_APP_NAME = "AUDIO_DEVICE_APP_NAME";

        public const string HINT_AUDIO_DEVICE_STREAM_NAME = "AUDIO_DEVICE_STREAM_NAME";

        public const string HINT_PREFERRED_LOCALES = "PREFERRED_LOCALES";

        public const string HINT_THREAD_PRIORITY_POLICY = "THREAD_PRIORITY_POLICY";

        public const string HINT_EMSCRIPTEN_ASYNCIFY = "EMSCRIPTEN_ASYNCIFY";

        public const string HINT_LINUX_JOYSTICK_DEADZONES = "LINUX_JOYSTICK_DEADZONES";

        public const string HINT_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO = "ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";

        public const string HINT_JOYSTICK_HIDAPI_PS5 = "JOYSTICK_HIDAPI_PS5";

        public const string HINT_THREAD_FORCE_REALTIME_TIME_CRITICAL = "THREAD_FORCE_REALTIME_TIME_CRITICAL";

        public const string HINT_JOYSTICK_THREAD = "JOYSTICK_THREAD";

        public const string HINT_AUTO_UPDATE_JOYSTICKS = "AUTO_UPDATE_JOYSTICKS";

        public const string HINT_AUTO_UPDATE_SENSORS = "AUTO_UPDATE_SENSORS";

        public const string HINT_MOUSE_RELATIVE_SCALING = "MOUSE_RELATIVE_SCALING";

        public const string HINT_JOYSTICK_HIDAPI_PS5_RUMBLE = "JOYSTICK_HIDAPI_PS5_RUMBLE";

        public const int MAJOR_VERSION = 2;

        public const int MINOR_VERSION = 0;

        public const int PATCHLEVEL = 14;

        static public readonly int COMPILEDVERSION = VERSIONNUM(2, 0, 14);

        public const int WINDOWPOS_UNDEFINED_MASK = 536805376;

        public const int WINDOWPOS_CENTERED_MASK = 805240832;

        public const int WINDOWPOS_UNDEFINED = 536805376;

        public const int WINDOWPOS_CENTERED = 805240832;

        static public readonly uint PIXELFORMAT_UNKNOWN = 0u;

        static public readonly uint PIXELFORMAT_INDEX1LSB = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX1, 1u, PackedLayout.PACKEDLAYOUT_NONE, 1, 0);

        static public readonly uint PIXELFORMAT_INDEX1MSB = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX1, 2u, PackedLayout.PACKEDLAYOUT_NONE, 1, 0);

        static public readonly uint PIXELFORMAT_INDEX4LSB = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX4, 1u, PackedLayout.PACKEDLAYOUT_NONE, 4, 0);

        static public readonly uint PIXELFORMAT_INDEX4MSB = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX4, 2u, PackedLayout.PACKEDLAYOUT_NONE, 4, 0);

        static public readonly uint PIXELFORMAT_INDEX8 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX8, 0u, PackedLayout.PACKEDLAYOUT_NONE, 8, 1);

        static public readonly uint PIXELFORMAT_RGB332 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED8, 1u, PackedLayout.PACKEDLAYOUT_332, 8, 1);

        static public readonly uint PIXELFORMAT_XRGB444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 1u, PackedLayout.PACKEDLAYOUT_4444, 12, 2);

        static public readonly uint PIXELFORMAT_RGB444 = PIXELFORMAT_XRGB444;

        static public readonly uint PIXELFORMAT_XBGR444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 5u, PackedLayout.PACKEDLAYOUT_4444, 12, 2);

        static public readonly uint PIXELFORMAT_BGR444 = PIXELFORMAT_XBGR444;

        static public readonly uint PIXELFORMAT_XRGB1555 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 1u, PackedLayout.PACKEDLAYOUT_1555, 15, 2);

        static public readonly uint PIXELFORMAT_RGB555 = PIXELFORMAT_XRGB1555;

        static public readonly uint PIXELFORMAT_XBGR1555 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_INDEX1, 1u, PackedLayout.PACKEDLAYOUT_1555, 15, 2);

        static public readonly uint PIXELFORMAT_BGR555 = PIXELFORMAT_XBGR1555;

        static public readonly uint PIXELFORMAT_ARGB4444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 3u, PackedLayout.PACKEDLAYOUT_4444, 16, 2);

        static public readonly uint PIXELFORMAT_RGBA4444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 4u, PackedLayout.PACKEDLAYOUT_4444, 16, 2);

        static public readonly uint PIXELFORMAT_ABGR4444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 7u, PackedLayout.PACKEDLAYOUT_4444, 16, 2);

        static public readonly uint PIXELFORMAT_BGRA4444 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 8u, PackedLayout.PACKEDLAYOUT_4444, 16, 2);

        static public readonly uint PIXELFORMAT_ARGB1555 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 3u, PackedLayout.PACKEDLAYOUT_1555, 16, 2);

        static public readonly uint PIXELFORMAT_RGBA5551 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 4u, PackedLayout.PACKEDLAYOUT_5551, 16, 2);

        static public readonly uint PIXELFORMAT_ABGR1555 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 7u, PackedLayout.PACKEDLAYOUT_1555, 16, 2);

        static public readonly uint PIXELFORMAT_BGRA5551 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 8u, PackedLayout.PACKEDLAYOUT_5551, 16, 2);

        static public readonly uint PIXELFORMAT_RGB565 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 1u, PackedLayout.PACKEDLAYOUT_565, 16, 2);

        static public readonly uint PIXELFORMAT_BGR565 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED16, 5u, PackedLayout.PACKEDLAYOUT_565, 16, 2);

        static public readonly uint PIXELFORMAT_RGB24 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_ARRAYU8, 1u, PackedLayout.PACKEDLAYOUT_NONE, 24, 3);

        static public readonly uint PIXELFORMAT_BGR24 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_ARRAYU8, 4u, PackedLayout.PACKEDLAYOUT_NONE, 24, 3);

        static public readonly uint PIXELFORMAT_XRGB888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 1u, PackedLayout.PACKEDLAYOUT_8888, 24, 4);

        static public readonly uint PIXELFORMAT_RGB888 = PIXELFORMAT_XRGB888;

        static public readonly uint PIXELFORMAT_RGBX8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 2u, PackedLayout.PACKEDLAYOUT_8888, 24, 4);

        static public readonly uint PIXELFORMAT_XBGR888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 5u, PackedLayout.PACKEDLAYOUT_8888, 24, 4);

        static public readonly uint PIXELFORMAT_BGR888 = PIXELFORMAT_XBGR888;

        static public readonly uint PIXELFORMAT_BGRX8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 6u, PackedLayout.PACKEDLAYOUT_8888, 24, 4);

        static public readonly uint PIXELFORMAT_ARGB8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 3u, PackedLayout.PACKEDLAYOUT_8888, 32, 4);

        static public readonly uint PIXELFORMAT_RGBA8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 4u, PackedLayout.PACKEDLAYOUT_8888, 32, 4);

        static public readonly uint PIXELFORMAT_ABGR8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 7u, PackedLayout.PACKEDLAYOUT_8888, 32, 4);

        static public readonly uint PIXELFORMAT_BGRA8888 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 8u, PackedLayout.PACKEDLAYOUT_8888, 32, 4);

        static public readonly uint PIXELFORMAT_ARGB2101010 = DEFINE_PIXELFORMAT(PixelType.PIXELTYPE_PACKED32, 3u, PackedLayout.PACKEDLAYOUT_2101010, 32, 4);

        static public readonly uint PIXELFORMAT_YV12 = DEFINE_PIXELFOURCC(89, 86, 49, 50);

        static public readonly uint PIXELFORMAT_IYUV = DEFINE_PIXELFOURCC(73, 89, 85, 86);

        static public readonly uint PIXELFORMAT_YUY2 = DEFINE_PIXELFOURCC(89, 85, 89, 50);

        static public readonly uint PIXELFORMAT_UYVY = DEFINE_PIXELFOURCC(85, 89, 86, 89);

        static public readonly uint PIXELFORMAT_YVYU = DEFINE_PIXELFOURCC(89, 86, 89, 85);

        public const uint SWSURFACE = 0u;

        public const uint PREALLOC = 1u;

        public const uint RLEACCEL = 2u;

        public const uint DONTFREE = 4u;

        public const byte PRESSED = 1;

        public const byte RELEASED = 0;

        public const int TEXTEDITINGEVENT_TEXT_SIZE = 32;

        public const int TEXTINPUTEVENT_TEXT_SIZE = 32;

        public const int QUERY = -1;

        public const int IGNORE = 0;

        public const int DISABLE = 0;

        public const int ENABLE = 1;

        public const int SDLK_SCANCODE_MASK = 1073741824;

        public const uint BUTTON_LEFT = 1u;

        public const uint BUTTON_MIDDLE = 2u;

        public const uint BUTTON_RIGHT = 3u;

        public const uint BUTTON_X1 = 4u;

        public const uint BUTTON_X2 = 5u;

        static public readonly uint BUTTON_LMASK = BUTTON(1u);

        static public readonly uint BUTTON_MMASK = BUTTON(2u);

        static public readonly uint BUTTON_RMASK = BUTTON(3u);

        static public readonly uint BUTTON_X1MASK = BUTTON(4u);

        static public readonly uint BUTTON_X2MASK = BUTTON(5u);

        public const uint TOUCH_MOUSEID = uint.MaxValue;

        public const byte HAT_CENTERED = 0;

        public const byte HAT_UP = 1;

        public const byte HAT_RIGHT = 2;

        public const byte HAT_DOWN = 4;

        public const byte HAT_LEFT = 8;

        public const byte HAT_RIGHTUP = 3;

        public const byte HAT_RIGHTDOWN = 6;

        public const byte HAT_LEFTUP = 9;

        public const byte HAT_LEFTDOWN = 12;

        public const float IPHONE_MAX_GFORCE = 5f;

        public const ushort HAPTIC_CONSTANT = 1;

        public const ushort HAPTIC_SINE = 2;

        public const ushort HAPTIC_LEFTRIGHT = 4;

        public const ushort HAPTIC_TRIANGLE = 8;

        public const ushort HAPTIC_SAWTOOTHUP = 16;

        public const ushort HAPTIC_SAWTOOTHDOWN = 32;

        public const ushort HAPTIC_SPRING = 128;

        public const ushort HAPTIC_DAMPER = 256;

        public const ushort HAPTIC_INERTIA = 512;

        public const ushort HAPTIC_FRICTION = 1024;

        public const ushort HAPTIC_CUSTOM = 2048;

        public const ushort HAPTIC_GAIN = 4096;

        public const ushort HAPTIC_AUTOCENTER = 8192;

        public const ushort HAPTIC_STATUS = 16384;

        public const ushort HAPTIC_PAUSE = 32768;

        public const byte HAPTIC_POLAR = 0;

        public const byte HAPTIC_CARTESIAN = 1;

        public const byte HAPTIC_SPHERICAL = 2;

        public const byte HAPTIC_STEERING_AXIS = 3;

        public const uint HAPTIC_INFINITY = uint.MaxValue;

        public const float STANDARD_GRAVITY = 9.80665f;

        public const ushort AUDIO_MASK_BITSIZE = 255;

        public const ushort AUDIO_MASK_DATATYPE = 256;

        public const ushort AUDIO_MASK_ENDIAN = 4096;

        public const ushort AUDIO_MASK_SIGNED = 32768;

        public const ushort AUDIO_U8 = 8;

        public const ushort AUDIO_S8 = 32776;

        public const ushort AUDIO_U16LSB = 16;

        public const ushort AUDIO_S16LSB = 32784;

        public const ushort AUDIO_U16MSB = 4112;

        public const ushort AUDIO_S16MSB = 36880;

        public const ushort AUDIO_U16 = 16;

        public const ushort AUDIO_S16 = 32784;

        public const ushort AUDIO_S32LSB = 32800;

        public const ushort AUDIO_S32MSB = 36896;

        public const ushort AUDIO_S32 = 32800;

        public const ushort AUDIO_F32LSB = 33056;

        public const ushort AUDIO_F32MSB = 37152;

        public const ushort AUDIO_F32 = 33056;

        static public readonly ushort AUDIO_U16SYS = (ushort)(BitConverter.IsLittleEndian ? 16 : 4112);

        static public readonly ushort AUDIO_S16SYS = (ushort)(BitConverter.IsLittleEndian ? 32784 : 36880);

        static public readonly ushort AUDIO_S32SYS = (ushort)(BitConverter.IsLittleEndian ? 32800 : 36896);

        static public readonly ushort AUDIO_F32SYS = (ushort)(BitConverter.IsLittleEndian ? 33056 : 37152);

        public const uint AUDIO_ALLOW_FREQUENCY_CHANGE = 1u;

        public const uint AUDIO_ALLOW_FORMAT_CHANGE = 2u;

        public const uint AUDIO_ALLOW_CHANNELS_CHANGE = 4u;

        public const uint AUDIO_ALLOW_SAMPLES_CHANGE = 8u;

        public const uint AUDIO_ALLOW_ANY_CHANGE = 15u;

        public const int MIX_MAXVOLUME = 128;

        public const int ANDROID_EXTERNAL_STORAGE_READ = 1;

        public const int ANDROID_EXTERNAL_STORAGE_WRITE = 2;

        internal static int Utf8Size(string str)
        {
            return str.Length * 4 + 1;
        }

        internal static int Utf8SizeNullable(string str)
        {
            if (str == null)
            {
                return 0;
            }

            return str.Length * 4 + 1;
        }

        internal unsafe static byte* Utf8Encode(string str, byte* buffer, int bufferSize)
        {
            fixed (char* chars = str)
            {
                Encoding.UTF8.GetBytes(chars, str.Length + 1, buffer, bufferSize);
            }

            return buffer;
        }

        internal unsafe static byte* Utf8EncodeNullable(string str, byte* buffer, int bufferSize)
        {
            if (str == null)
            {
                return null;
            }

            fixed (char* chars = str)
            {
                Encoding.UTF8.GetBytes(chars, str.Length + 1, buffer, bufferSize);
            }

            return buffer;
        }

        internal unsafe static byte* Utf8Encode(string str)
        {
            int num = Utf8Size(str);
            byte* ptr = (byte*)(void*)Marshal.AllocHGlobal(num);
            fixed (char* chars = str)
            {
                Encoding.UTF8.GetBytes(chars, str.Length + 1, ptr, num);
            }

            return ptr;
        }

        public unsafe static string UTF8_ToManaged(IntPtr s, bool freePtr = false)
        {
            if (s == IntPtr.Zero)
            {
                return null;
            }

            byte* ptr;
            for (ptr = (byte*)(void*)s; *ptr != 0; ptr++)
            {
            }

            string @string = Encoding.UTF8.GetString((byte*)(void*)s, (int)(ptr - (byte*)(void*)s));
            if (freePtr)
            {
                free(s);
            }

            return @string;
        }

        static public uint FOURCC(byte A, byte B, byte C, byte D)
        {
            return (uint)(A | (B << 8) | (C << 16) | (D << 24));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_malloc")]
        internal static extern IntPtr malloc(IntPtr size);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_free")]
        internal static extern void free(IntPtr memblock);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_memcpy")]
        static public extern IntPtr memcpy(void* dst, void* src, IntPtr len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWFromFile")]
        private unsafe static extern IntPtr INTERNAL_RWFromFile(byte* file, byte* mode);

        public unsafe static IntPtr RWFromFile(string file, string mode)
        {
            byte* intPtr = Utf8Encode(file);
            byte* ptr = Utf8Encode(mode);
            IntPtr result = INTERNAL_RWFromFile(intPtr, ptr);
            Marshal.FreeHGlobal((IntPtr)ptr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AllocRW")]
        static public extern IntPtr AllocRW();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeRW")]
        static public extern void FreeRW(IntPtr area);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWFromFP")]
        static public extern IntPtr RWFromFP(IntPtr fp, SDL_bool autoclose);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWFromMem")]
        static public extern IntPtr RWFromMem(IntPtr mem, int size);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWFromConstMem")]
        static public extern IntPtr RWFromConstMem(IntPtr mem, int size);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWsize")]
        static public extern long RWsize(IntPtr context);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWseek")]
        static public extern long RWseek(IntPtr context, long offset, int whence);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWtell")]
        static public extern long RWtell(IntPtr context);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWread")]
        static public extern long RWread(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWwrite")]
        static public extern long RWwrite(IntPtr context, IntPtr ptr, IntPtr size, IntPtr maxnum);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadU8")]
        static public extern byte ReadU8(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadLE16")]
        static public extern ushort ReadLE16(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadBE16")]
        static public extern ushort ReadBE16(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadLE32")]
        static public extern uint ReadLE32(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadBE32")]
        static public extern uint ReadBE32(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadLE64")]
        static public extern ulong ReadLE64(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ReadBE64")]
        static public extern ulong ReadBE64(IntPtr src);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteU8")]
        static public extern uint WriteU8(IntPtr dst, byte value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteLE16")]
        static public extern uint WriteLE16(IntPtr dst, ushort value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteBE16")]
        static public extern uint WriteBE16(IntPtr dst, ushort value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteLE32")]
        static public extern uint WriteLE32(IntPtr dst, uint value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteBE32")]
        static public extern uint WriteBE32(IntPtr dst, uint value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteLE64")]
        static public extern uint WriteLE64(IntPtr dst, ulong value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WriteBE64")]
        static public extern uint WriteBE64(IntPtr dst, ulong value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RWclose")]
        static public extern long RWclose(IntPtr context);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LoadFile")]
        private unsafe static extern IntPtr INTERNAL_LoadFile(byte* file, out IntPtr datasize);

        public unsafe static IntPtr LoadFile(string file, out IntPtr datasize)
        {
            byte* intPtr = Utf8Encode(file);
            IntPtr result = INTERNAL_LoadFile(intPtr, out datasize);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetMainReady")]
        static public extern void SetMainReady();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WinRTRunApp")]
        static public extern int WinRTRunApp(main_func mainFunction, IntPtr reserved);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UIKitRunApp")]
        static public extern int UIKitRunApp(int argc, IntPtr argv, main_func mainFunction);

        /// <summary>
        /// Initialize the SDL library.
        /// </summary>
        /// <param name="flags">subsystem initialization flags</param>
        /// <returns>Returns 0 on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Init")]
        static public extern int Init([MarshalAs(UnmanagedType.U4)] InitFlags flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_InitSubSystem")]
        static public extern int InitSubSystem(uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Quit")]
        static public extern void Quit();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_QuitSubSystem")]
        static public extern void QuitSubSystem(uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WasInit")]
        static public extern uint WasInit(uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPlatform")]
        private static extern IntPtr INTERNAL_GetPlatform();

        static public string GetPlatform()
        {
            return UTF8_ToManaged(INTERNAL_GetPlatform());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ClearHints")]
        static public extern void ClearHints();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetHint")]
        private unsafe static extern IntPtr INTERNAL_GetHint(byte* name);

        public unsafe static string GetHint(string name)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return UTF8_ToManaged(INTERNAL_GetHint(Utf8Encode(name, buffer, num)));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetHint")]
        private unsafe static extern SDL_bool INTERNAL_SetHint(byte* name, byte* value);

        public unsafe static SDL_bool SetHint(string name, string value)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            int num2 = Utf8Size(value);
            byte* buffer2 = stackalloc byte[(int)(uint)num2];
            return INTERNAL_SetHint(Utf8Encode(name, buffer, num), Utf8Encode(value, buffer2, num2));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetHintWithPriority")]
        private unsafe static extern SDL_bool INTERNAL_SetHintWithPriority(byte* name, byte* value, HintPriority priority);

        public unsafe static SDL_bool SetHintWithPriority(string name, string value, HintPriority priority)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            int num2 = Utf8Size(value);
            byte* buffer2 = stackalloc byte[(int)(uint)num2];
            return INTERNAL_SetHintWithPriority(Utf8Encode(name, buffer, num), Utf8Encode(value, buffer2, num2), priority);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetHintBoolean")]
        private unsafe static extern SDL_bool INTERNAL_GetHintBoolean(byte* name, SDL_bool default_value);

        public unsafe static SDL_bool GetHintBoolean(string name, SDL_bool default_value)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GetHintBoolean(Utf8Encode(name, buffer, num), default_value);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ClearError")]
        static public extern void ClearError();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetError")]
        private static extern IntPtr INTERNAL_GetError();

        /// <summary>
        /// Retrieve a message about the last error that occurred on the current thread.
        /// </summary>
        /// <returns>Returns a message with information about the specific error that occurred, or an empty string if there hasn't been an error message set since the last call to <see cref="ClearError"/>. The message is only applicable when an SDL function has signaled an error. You must check the return values of SDL function calls to determine when to appropriately call <see cref="GetError"/>.</returns>
        static public string GetError()
        {
            return UTF8_ToManaged(INTERNAL_GetError());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetError")]
        private unsafe static extern void INTERNAL_SetError(byte* fmtAndArglist);

        public unsafe static void SetError(string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_SetError(Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetErrorMsg")]
        static public extern IntPtr GetErrorMsg(IntPtr errstr, int maxlength);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Log")]
        private unsafe static extern void INTERNAL_Log(byte* fmtAndArglist);

        public unsafe static void Log(string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_Log(Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogVerbose")]
        private unsafe static extern void INTERNAL_LogVerbose(int category, byte* fmtAndArglist);

        public unsafe static void LogVerbose(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogVerbose(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogDebug")]
        private unsafe static extern void INTERNAL_LogDebug(int category, byte* fmtAndArglist);

        public unsafe static void LogDebug(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogDebug(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogInfo")]
        private unsafe static extern void INTERNAL_LogInfo(int category, byte* fmtAndArglist);

        public unsafe static void LogInfo(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogInfo(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogWarn")]
        private unsafe static extern void INTERNAL_LogWarn(int category, byte* fmtAndArglist);

        public unsafe static void LogWarn(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogWarn(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogError")]
        private unsafe static extern void INTERNAL_LogError(int category, byte* fmtAndArglist);

        public unsafe static void LogError(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogError(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogCritical")]
        private unsafe static extern void INTERNAL_LogCritical(int category, byte* fmtAndArglist);

        public unsafe static void LogCritical(int category, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogCritical(category, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogMessage")]
        private unsafe static extern void INTERNAL_LogMessage(int category, LogPriority priority, byte* fmtAndArglist);

        public unsafe static void LogMessage(int category, LogPriority priority, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogMessage(category, priority, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogMessageV")]
        private unsafe static extern void INTERNAL_LogMessageV(int category, LogPriority priority, byte* fmtAndArglist);

        public unsafe static void LogMessageV(int category, LogPriority priority, string fmtAndArglist)
        {
            int num = Utf8Size(fmtAndArglist);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_LogMessageV(category, priority, Utf8Encode(fmtAndArglist, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogGetPriority")]
        static public extern LogPriority LogGetPriority(int category);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogSetPriority")]
        static public extern void LogSetPriority(int category, LogPriority priority);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogSetAllPriority")]
        static public extern void LogSetAllPriority(LogPriority priority);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogResetPriorities")]
        static public extern void LogResetPriorities();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogGetOutputFunction")]
        private static extern void LogGetOutputFunction(out IntPtr callback, out IntPtr userdata);

        static public void LogGetOutputFunction(out LogOutputFunction callback, out IntPtr userdata)
        {
            IntPtr callback2 = IntPtr.Zero;
            LogGetOutputFunction(out callback2, out userdata);
            if (callback2 != IntPtr.Zero)
            {
                callback = (LogOutputFunction)Marshal.GetDelegateForFunctionPointer(callback2, typeof(LogOutputFunction));
            }
            else
            {
                callback = null;
            }
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LogSetOutputFunction")]
        static public extern void LogSetOutputFunction(LogOutputFunction callback, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ShowMessageBox")]
        private static extern int INTERNAL_ShowMessageBox([In] ref INTERNAL_MessageBoxData messageboxdata, out int buttonid);

        private static IntPtr INTERNAL_AllocUTF8(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return IntPtr.Zero;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(str + "\0");
            IntPtr intPtr = malloc((IntPtr)bytes.Length);
            Marshal.Copy(bytes, 0, intPtr, bytes.Length);
            return intPtr;
        }

        public unsafe static int ShowMessageBox([In] ref MessageBoxData messageboxdata, out int buttonid)
        {
            INTERNAL_MessageBoxData iNTERNAL_MessageBoxData = default(INTERNAL_MessageBoxData);
            iNTERNAL_MessageBoxData.flags = messageboxdata.flags;
            iNTERNAL_MessageBoxData.window = messageboxdata.window;
            iNTERNAL_MessageBoxData.title = INTERNAL_AllocUTF8(messageboxdata.title);
            iNTERNAL_MessageBoxData.message = INTERNAL_AllocUTF8(messageboxdata.message);
            iNTERNAL_MessageBoxData.numbuttons = messageboxdata.numbuttons;
            INTERNAL_MessageBoxData messageboxdata2 = iNTERNAL_MessageBoxData;
            INTERNAL_MessageBoxButtonData[] array = new INTERNAL_MessageBoxButtonData[messageboxdata.numbuttons];
            for (int i = 0; i < messageboxdata.numbuttons; i++)
            {
                array[i] = new INTERNAL_MessageBoxButtonData
                {
                    flags = messageboxdata.buttons[i].flags,
                    buttonid = messageboxdata.buttons[i].buttonid,
                    text = INTERNAL_AllocUTF8(messageboxdata.buttons[i].text)
                };
            }

            if (messageboxdata.colorScheme.HasValue)
            {
                messageboxdata2.colorScheme = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MessageBoxColorScheme)));
                Marshal.StructureToPtr(messageboxdata.colorScheme.Value, messageboxdata2.colorScheme, fDeleteOld: false);
            }

            int result;
            fixed (INTERNAL_MessageBoxButtonData* ptr = &array[0])
            {
                messageboxdata2.buttons = (IntPtr)ptr;
                result = INTERNAL_ShowMessageBox(ref messageboxdata2, out buttonid);
            }

            Marshal.FreeHGlobal(messageboxdata2.colorScheme);
            for (int j = 0; j < messageboxdata.numbuttons; j++)
            {
                free(array[j].text);
            }

            free(messageboxdata2.message);
            free(messageboxdata2.title);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ShowSimpleMessageBox")]
        private unsafe static extern int INTERNAL_ShowSimpleMessageBox(MessageBoxFlags flags, byte* title, byte* message, IntPtr window);

        public unsafe static int ShowSimpleMessageBox(MessageBoxFlags flags, string title, string message, IntPtr window)
        {
            int num = Utf8SizeNullable(title);
            byte* buffer = stackalloc byte[(int)(uint)num];
            int num2 = Utf8SizeNullable(message);
            byte* buffer2 = stackalloc byte[(int)(uint)num2];
            return INTERNAL_ShowSimpleMessageBox(flags, Utf8EncodeNullable(title, buffer, num), Utf8EncodeNullable(message, buffer2, num2), window);
        }

        static public void VERSION(out version x)
        {
            x.major = 2;
            x.minor = 0;
            x.patch = 14;
        }

        static public int VERSIONNUM(int X, int Y, int Z)
        {
            return X * 1000 + Y * 100 + Z;
        }

        static public bool VERSION_ATLEAST(int X, int Y, int Z)
        {
            return COMPILEDVERSION >= VERSIONNUM(X, Y, Z);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetVersion")]
        static public extern void GetVersion(out version ver);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRevision")]
        private static extern IntPtr INTERNAL_GetRevision();

        static public string GetRevision()
        {
            return UTF8_ToManaged(INTERNAL_GetRevision());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRevisionNumber")]
        static public extern int GetRevisionNumber();

        static public int WINDOWPOS_UNDEFINED_DISPLAY(int X)
        {
            return 0x1FFF0000 | X;
        }

        static public bool WINDOWPOS_ISUNDEFINED(int X)
        {
            return (X & 0xFFFF0000u) == 536805376;
        }

        static public int WINDOWPOS_CENTERED_DISPLAY(int X)
        {
            return 0x2FFF0000 | X;
        }

        static public bool WINDOWPOS_ISCENTERED(int X)
        {
            return (X & 0xFFFF0000u) == 805240832;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateWindow")]
        private unsafe static extern IntPtr INTERNAL_CreateWindow(byte* title, int x, int y, int w, int h, WindowFlags flags);

        public unsafe static IntPtr CreateWindow(string title, int x, int y, int w, int h, WindowFlags flags)
        {
            int num = Utf8SizeNullable(title);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_CreateWindow(Utf8EncodeNullable(title, buffer, num), x, y, w, h, flags);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateWindowAndRenderer")]
        static public extern int CreateWindowAndRenderer(int width, int height, WindowFlags window_flags, out IntPtr window, out IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateWindowFrom")]
        static public extern IntPtr CreateWindowFrom(IntPtr data);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DestroyWindow")]
        static public extern void DestroyWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DisableScreenSaver")]
        static public extern void DisableScreenSaver();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_EnableScreenSaver")]
        static public extern void EnableScreenSaver();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetClosestDisplayMode")]
        static public extern IntPtr GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCurrentDisplayMode")]
        static public extern int GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCurrentVideoDriver")]
        private static extern IntPtr INTERNAL_GetCurrentVideoDriver();

        static public string GetCurrentVideoDriver()
        {
            return UTF8_ToManaged(INTERNAL_GetCurrentVideoDriver());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDesktopDisplayMode")]
        static public extern int GetDesktopDisplayMode(int displayIndex, out DisplayMode mode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayName")]
        private static extern IntPtr INTERNAL_GetDisplayName(int index);

        static public string GetDisplayName(int index)
        {
            return UTF8_ToManaged(INTERNAL_GetDisplayName(index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayBounds")]
        static public extern int GetDisplayBounds(int displayIndex, out Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayDPI")]
        static public extern int GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayOrientation")]
        static public extern DisplayOrientation GetDisplayOrientation(int displayIndex);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayMode")]
        static public extern int GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetDisplayUsableBounds")]
        static public extern int GetDisplayUsableBounds(int displayIndex, out Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumDisplayModes")]
        static public extern int GetNumDisplayModes(int displayIndex);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumVideoDisplays")]
        static public extern int GetNumVideoDisplays();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumVideoDrivers")]
        static public extern int GetNumVideoDrivers();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetVideoDriver")]
        private static extern IntPtr INTERNAL_GetVideoDriver(int index);

        static public string GetVideoDriver(int index)
        {
            return UTF8_ToManaged(INTERNAL_GetVideoDriver(index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowBrightness")]
        static public extern float GetWindowBrightness(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowOpacity")]
        static public extern int SetWindowOpacity(IntPtr window, float opacity);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowOpacity")]
        static public extern int GetWindowOpacity(IntPtr window, out float out_opacity);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowModalFor")]
        static public extern int SetWindowModalFor(IntPtr modal_window, IntPtr parent_window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowInputFocus")]
        static public extern int SetWindowInputFocus(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowData")]
        private unsafe static extern IntPtr INTERNAL_GetWindowData(IntPtr window, byte* name);

        public unsafe static IntPtr GetWindowData(IntPtr window, string name)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GetWindowData(window, Utf8Encode(name, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowDisplayIndex")]
        static public extern int GetWindowDisplayIndex(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowDisplayMode")]
        static public extern int GetWindowDisplayMode(IntPtr window, out DisplayMode mode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowFlags")]
        static public extern uint GetWindowFlags(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowFromID")]
        static public extern IntPtr GetWindowFromID(uint id);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowGammaRamp")]
        static public extern int GetWindowGammaRamp(IntPtr window, [Out][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] red, [Out][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] green, [Out][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] blue);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowGrab")]
        static public extern SDL_bool GetWindowGrab(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowID")]
        static public extern uint GetWindowID(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowPixelFormat")]
        static public extern uint GetWindowPixelFormat(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowMaximumSize")]
        static public extern void GetWindowMaximumSize(IntPtr window, out int max_w, out int max_h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowMinimumSize")]
        static public extern void GetWindowMinimumSize(IntPtr window, out int min_w, out int min_h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowPosition")]
        static public extern void GetWindowPosition(IntPtr window, out int x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowSize")]
        static public extern void GetWindowSize(IntPtr window, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowSurface")]
        static public extern IntPtr GetWindowSurface(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowTitle")]
        private static extern IntPtr INTERNAL_GetWindowTitle(IntPtr window);

        static public string GetWindowTitle(IntPtr window)
        {
            return UTF8_ToManaged(INTERNAL_GetWindowTitle(window));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_BindTexture")]
        static public extern int GL_BindTexture(IntPtr texture, out float texw, out float texh);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_CreateContext")]
        static public extern IntPtr GL_CreateContext(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_DeleteContext")]
        static public extern void GL_DeleteContext(IntPtr context);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_LoadLibrary")]
        private unsafe static extern int INTERNAL_GL_LoadLibrary(byte* path);

        public unsafe static int GL_LoadLibrary(string path)
        {
            byte* intPtr = Utf8Encode(path);
            int result = INTERNAL_GL_LoadLibrary(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetProcAddress")]
        static public extern IntPtr GL_GetProcAddress(IntPtr proc);

        public unsafe static IntPtr GL_GetProcAddress(string proc)
        {
            int num = Utf8Size(proc);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return GL_GetProcAddress((IntPtr)Utf8Encode(proc, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_UnloadLibrary")]
        static public extern void GL_UnloadLibrary();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_ExtensionSupported")]
        private unsafe static extern SDL_bool INTERNAL_GL_ExtensionSupported(byte* extension);

        public unsafe static SDL_bool GL_ExtensionSupported(string extension)
        {
            int num = Utf8SizeNullable(extension);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GL_ExtensionSupported(Utf8Encode(extension, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_ResetAttributes")]
        static public extern void GL_ResetAttributes();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetAttribute")]
        static public extern int GL_GetAttribute(GLattr attr, out int value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetSwapInterval")]
        static public extern int GL_GetSwapInterval();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_MakeCurrent")]
        static public extern int GL_MakeCurrent(IntPtr window, IntPtr context);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetCurrentWindow")]
        static public extern IntPtr GL_GetCurrentWindow();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetCurrentContext")]
        static public extern IntPtr GL_GetCurrentContext();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_GetDrawableSize")]
        static public extern void GL_GetDrawableSize(IntPtr window, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_SetAttribute")]
        static public extern int GL_SetAttribute(GLattr attr, int value);

        static public int GL_SetAttribute(GLattr attr, GLprofile profile)
        {
            return GL_SetAttribute(attr, (int)profile);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_SetSwapInterval")]
        static public extern int GL_SetSwapInterval(int interval);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_SwapWindow")]
        static public extern void GL_SwapWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GL_UnbindTexture")]
        static public extern int GL_UnbindTexture(IntPtr texture);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HideWindow")]
        static public extern void HideWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsScreenSaverEnabled")]
        static public extern SDL_bool IsScreenSaverEnabled();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MaximizeWindow")]
        static public extern void MaximizeWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MinimizeWindow")]
        static public extern void MinimizeWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RaiseWindow")]
        static public extern void RaiseWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RestoreWindow")]
        static public extern void RestoreWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowBrightness")]
        static public extern int SetWindowBrightness(IntPtr window, float brightness);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowData")]
        private unsafe static extern IntPtr INTERNAL_SetWindowData(IntPtr window, byte* name, IntPtr userdata);

        public unsafe static IntPtr SetWindowData(IntPtr window, string name, IntPtr userdata)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_SetWindowData(window, Utf8Encode(name, buffer, num), userdata);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowDisplayMode")]
        static public extern int SetWindowDisplayMode(IntPtr window, ref DisplayMode mode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowFullscreen")]
        static public extern int SetWindowFullscreen(IntPtr window, uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowGammaRamp")]
        static public extern int SetWindowGammaRamp(IntPtr window, [In][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] red, [In][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] green, [In][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] blue);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowGrab")]
        static public extern void SetWindowGrab(IntPtr window, SDL_bool grabbed);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowIcon")]
        static public extern void SetWindowIcon(IntPtr window, IntPtr icon);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowMaximumSize")]
        static public extern void SetWindowMaximumSize(IntPtr window, int max_w, int max_h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowMinimumSize")]
        static public extern void SetWindowMinimumSize(IntPtr window, int min_w, int min_h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowPosition")]
        static public extern void SetWindowPosition(IntPtr window, int x, int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowSize")]
        static public extern void SetWindowSize(IntPtr window, int w, int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowBordered")]
        static public extern void SetWindowBordered(IntPtr window, SDL_bool bordered);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowBordersSize")]
        static public extern int GetWindowBordersSize(IntPtr window, out int top, out int left, out int bottom, out int right);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowResizable")]
        static public extern void SetWindowResizable(IntPtr window, SDL_bool resizable);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowTitle")]
        private unsafe static extern void INTERNAL_SetWindowTitle(IntPtr window, byte* title);

        public unsafe static void SetWindowTitle(IntPtr window, string title)
        {
            int num = Utf8Size(title);
            byte* buffer = stackalloc byte[(int)(uint)num];
            INTERNAL_SetWindowTitle(window, Utf8Encode(title, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ShowWindow")]
        static public extern void ShowWindow(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpdateWindowSurface")]
        static public extern int UpdateWindowSurface(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
        static public extern int UpdateWindowSurfaceRects(IntPtr window, [In] Rect[] rects, int numrects);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_VideoInit")]
        private unsafe static extern int INTERNAL_VideoInit(byte* driver_name);

        public unsafe static int VideoInit(string driver_name)
        {
            int num = Utf8Size(driver_name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_VideoInit(Utf8Encode(driver_name, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_VideoQuit")]
        static public extern void VideoQuit();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowHitTest")]
        static public extern int SetWindowHitTest(IntPtr window, HitTest callback, IntPtr callback_data);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetGrabbedWindow")]
        static public extern IntPtr GetGrabbedWindow();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ComposeCustomBlendMode")]
        static public extern BlendMode ComposeCustomBlendMode(BlendFactor srcColorFactor, BlendFactor dstColorFactor, BlendOperation colorOperation, BlendFactor srcAlphaFactor, BlendFactor dstAlphaFactor, BlendOperation alphaOperation);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_LoadLibrary")]
        private unsafe static extern int INTERNAL_Vulkan_LoadLibrary(byte* path);

        public unsafe static int Vulkan_LoadLibrary(string path)
        {
            byte* intPtr = Utf8Encode(path);
            int result = INTERNAL_Vulkan_LoadLibrary(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_GetVkGetInstanceProcAddr")]
        static public extern IntPtr Vulkan_GetVkGetInstanceProcAddr();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_UnloadLibrary")]
        static public extern void Vulkan_UnloadLibrary();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_GetInstanceExtensions")]
        static public extern SDL_bool Vulkan_GetInstanceExtensions(IntPtr window, out uint pCount, IntPtr[] pNames);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_CreateSurface")]
        static public extern SDL_bool Vulkan_CreateSurface(IntPtr window, IntPtr instance, out ulong surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Vulkan_GetDrawableSize")]
        static public extern void Vulkan_GetDrawableSize(IntPtr window, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Metal_CreateView")]
        static public extern IntPtr Metal_CreateView(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Metal_DestroyView")]
        static public extern void Metal_DestroyView(IntPtr view);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Metal_GetLayer")]
        static public extern IntPtr Metal_GetLayer(IntPtr view);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Metal_GetDrawableSize")]
        static public extern void Metal_GetDrawableSize(IntPtr window, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateRenderer")]
        static public extern IntPtr CreateRenderer(IntPtr window, int index, RendererFlags flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateSoftwareRenderer")]
        static public extern IntPtr CreateSoftwareRenderer(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateTexture")]
        static public extern IntPtr CreateTexture(IntPtr renderer, uint format, int access, int w, int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateTextureFromSurface")]
        static public extern IntPtr CreateTextureFromSurface(IntPtr renderer, IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DestroyRenderer")]
        static public extern void DestroyRenderer(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DestroyTexture")]
        static public extern void DestroyTexture(IntPtr texture);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumRenderDrivers")]
        static public extern int GetNumRenderDrivers();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRenderDrawBlendMode")]
        static public extern int GetRenderDrawBlendMode(IntPtr renderer, out BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetTextureScaleMode")]
        static public extern int SetTextureScaleMode(IntPtr texture, ScaleMode scaleMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTextureScaleMode")]
        static public extern int GetTextureScaleMode(IntPtr texture, out ScaleMode scaleMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRenderDrawColor")]
        static public extern int GetRenderDrawColor(IntPtr renderer, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRenderDriverInfo")]
        static public extern int GetRenderDriverInfo(int index, out RendererInfo info);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRenderer")]
        static public extern IntPtr GetRenderer(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRendererInfo")]
        static public extern int GetRendererInfo(IntPtr renderer, out RendererInfo info);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRendererOutputSize")]
        static public extern int GetRendererOutputSize(IntPtr renderer, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTextureAlphaMod")]
        static public extern int GetTextureAlphaMod(IntPtr texture, out byte alpha);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTextureBlendMode")]
        static public extern int GetTextureBlendMode(IntPtr texture, out BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTextureColorMod")]
        static public extern int GetTextureColorMod(IntPtr texture, out byte r, out byte g, out byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockTexture")]
        static public extern int LockTexture(IntPtr texture, ref Rect rect, out IntPtr pixels, out int pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockTexture")]
        static public extern int LockTexture(IntPtr texture, IntPtr rect, out IntPtr pixels, out int pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockTextureToSurface")]
        static public extern int LockTextureToSurface(IntPtr texture, ref Rect rect, out IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockTextureToSurface")]
        static public extern int LockTextureToSurface(IntPtr texture, IntPtr rect, out IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_QueryTexture")]
        static public extern int QueryTexture(IntPtr texture, out uint format, out int access, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderClear")]
        static public extern int RenderClear(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopy")]
        static public extern int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopy")]
        static public extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopy")]
        static public extern int RenderCopy(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopy")]
        static public extern int RenderCopy(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref Rect dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, ref Point center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref Rect dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawLine")]
        static public extern int RenderDrawLine(IntPtr renderer, int x1, int y1, int x2, int y2);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawLines")]
        static public extern int RenderDrawLines(IntPtr renderer, [In] Point[] points, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawPoint")]
        static public extern int RenderDrawPoint(IntPtr renderer, int x, int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawPoints")]
        static public extern int RenderDrawPoints(IntPtr renderer, [In] Point[] points, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRect")]
        static public extern int RenderDrawRect(IntPtr renderer, ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRect")]
        static public extern int RenderDrawRect(IntPtr renderer, IntPtr rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRects")]
        static public extern int RenderDrawRects(IntPtr renderer, [In] Rect[] rects, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRect")]
        static public extern int RenderFillRect(IntPtr renderer, ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRect")]
        static public extern int RenderFillRect(IntPtr renderer, IntPtr rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRects")]
        static public extern int RenderFillRects(IntPtr renderer, [In] Rect[] rects, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyF")]
        static public extern int RenderCopyF(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref FRect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyF")]
        static public extern int RenderCopyF(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref FRect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyF")]
        static public extern int RenderCopyF(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyF")]
        static public extern int RenderCopyF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref FRect dstrect, double angle, ref FPoint center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyEx")]
        static public extern int RenderCopyEx(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref FRect dstrect, double angle, ref FPoint center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect, double angle, ref FPoint center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, ref Rect srcrect, ref FRect dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, ref FPoint center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, ref FRect dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, ref Rect srcrect, IntPtr dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderCopyExF")]
        static public extern int RenderCopyExF(IntPtr renderer, IntPtr texture, IntPtr srcrect, IntPtr dstrect, double angle, IntPtr center, RendererFlip flip);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawPointF")]
        static public extern int RenderDrawPointF(IntPtr renderer, float x, float y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawPointsF")]
        static public extern int RenderDrawPointsF(IntPtr renderer, [In] FPoint[] points, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawLineF")]
        static public extern int RenderDrawLineF(IntPtr renderer, float x1, float y1, float x2, float y2);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawLinesF")]
        static public extern int RenderDrawLinesF(IntPtr renderer, [In] FPoint[] points, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRectF")]
        static public extern int RenderDrawRectF(IntPtr renderer, ref FRect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRectF")]
        static public extern int RenderDrawRectF(IntPtr renderer, IntPtr rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderDrawRectsF")]
        static public extern int RenderDrawRectsF(IntPtr renderer, [In] FRect[] rects, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRectF")]
        static public extern int RenderFillRectF(IntPtr renderer, ref FRect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRectF")]
        static public extern int RenderFillRectF(IntPtr renderer, IntPtr rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFillRectsF")]
        static public extern int RenderFillRectsF(IntPtr renderer, [In] FRect[] rects, int count);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetClipRect")]
        static public extern void RenderGetClipRect(IntPtr renderer, out Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetLogicalSize")]
        static public extern void RenderGetLogicalSize(IntPtr renderer, out int w, out int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetScale")]
        static public extern void RenderGetScale(IntPtr renderer, out float scaleX, out float scaleY);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetViewport")]
        static public extern int RenderGetViewport(IntPtr renderer, out Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderPresent")]
        static public extern void RenderPresent(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderReadPixels")]
        static public extern int RenderReadPixels(IntPtr renderer, ref Rect rect, uint format, IntPtr pixels, int pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetClipRect")]
        static public extern int RenderSetClipRect(IntPtr renderer, ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetClipRect")]
        static public extern int RenderSetClipRect(IntPtr renderer, IntPtr rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetLogicalSize")]
        static public extern int RenderSetLogicalSize(IntPtr renderer, int w, int h);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetScale")]
        static public extern int RenderSetScale(IntPtr renderer, float scaleX, float scaleY);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetIntegerScale")]
        static public extern int RenderSetIntegerScale(IntPtr renderer, SDL_bool enable);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderSetViewport")]
        static public extern int RenderSetViewport(IntPtr renderer, ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetRenderDrawBlendMode")]
        static public extern int SetRenderDrawBlendMode(IntPtr renderer, BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetRenderDrawColor")]
        static public extern int SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetRenderTarget")]
        static public extern int SetRenderTarget(IntPtr renderer, IntPtr texture);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetTextureAlphaMod")]
        static public extern int SetTextureAlphaMod(IntPtr texture, byte alpha);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetTextureBlendMode")]
        static public extern int SetTextureBlendMode(IntPtr texture, BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetTextureColorMod")]
        static public extern int SetTextureColorMod(IntPtr texture, byte r, byte g, byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockTexture")]
        static public extern void UnlockTexture(IntPtr texture);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpdateTexture")]
        static public extern int UpdateTexture(IntPtr texture, ref Rect rect, IntPtr pixels, int pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpdateTexture")]
        static public extern int UpdateTexture(IntPtr texture, IntPtr rect, IntPtr pixels, int pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpdateYUVTexture")]
        static public extern int UpdateYUVTexture(IntPtr texture, ref Rect rect, IntPtr yPlane, int yPitch, IntPtr uPlane, int uPitch, IntPtr vPlane, int vPitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderTargetSupported")]
        static public extern SDL_bool RenderTargetSupported(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRenderTarget")]
        static public extern IntPtr GetRenderTarget(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetMetalLayer")]
        static public extern IntPtr RenderGetMetalLayer(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderGetMetalCommandEncoder")]
        static public extern IntPtr RenderGetMetalCommandEncoder(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderIsClipEnabled")]
        static public extern SDL_bool RenderIsClipEnabled(IntPtr renderer);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RenderFlush")]
        static public extern int RenderFlush(IntPtr renderer);

        static public uint DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
        {
            return FOURCC(A, B, C, D);
        }

        static public uint DEFINE_PIXELFORMAT(PixelType type, uint order, PackedLayout layout, byte bits, byte bytes)
        {
            return 0x10000000u | (uint)((byte)type << 24) | (uint)((byte)order << 20) | (uint)((byte)layout << 16) | (uint)(bits << 8) | bytes;
        }

        static public byte PIXELFLAG(uint X)
        {
            return (byte)((X >> 28) & 0xFu);
        }

        static public byte PIXELTYPE(uint X)
        {
            return (byte)((X >> 24) & 0xFu);
        }

        static public byte PIXELORDER(uint X)
        {
            return (byte)((X >> 20) & 0xFu);
        }

        static public byte PIXELLAYOUT(uint X)
        {
            return (byte)((X >> 16) & 0xFu);
        }

        static public byte BITSPERPIXEL(uint X)
        {
            return (byte)((X >> 8) & 0xFFu);
        }

        static public byte BYTESPERPIXEL(uint X)
        {
            if (ISPIXELFORMAT_FOURCC(X))
            {
                if (X == PIXELFORMAT_YUY2 || X == PIXELFORMAT_UYVY || X == PIXELFORMAT_YVYU)
                {
                    return 2;
                }

                return 1;
            }

            return (byte)(X & 0xFFu);
        }

        static public bool ISPIXELFORMAT_INDEXED(uint format)
        {
            if (ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }

            PixelType PixelType = (PixelType)PIXELTYPE(format);
            if (PixelType != PixelType.PIXELTYPE_INDEX1 && PixelType != PixelType.PIXELTYPE_INDEX4)
            {
                return PixelType == PixelType.PIXELTYPE_INDEX8;
            }

            return true;
        }

        static public bool ISPIXELFORMAT_PACKED(uint format)
        {
            if (ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }

            PixelType PixelType = (PixelType)PIXELTYPE(format);
            if (PixelType != PixelType.PIXELTYPE_PACKED8 && PixelType != PixelType.PIXELTYPE_PACKED16)
            {
                return PixelType == PixelType.PIXELTYPE_PACKED32;
            }

            return true;
        }

        static public bool ISPIXELFORMAT_ARRAY(uint format)
        {
            if (ISPIXELFORMAT_FOURCC(format))
            {
                return false;
            }

            PixelType PixelType = (PixelType)PIXELTYPE(format);
            if (PixelType != PixelType.PIXELTYPE_ARRAYU8 && PixelType != PixelType.PIXELTYPE_ARRAYU16 && PixelType != PixelType.PIXELTYPE_ARRAYU32 && PixelType != PixelType.PIXELTYPE_ARRAYF16)
            {
                return PixelType == PixelType.PIXELTYPE_ARRAYF32;
            }

            return true;
        }

        static public bool ISPIXELFORMAT_ALPHA(uint format)
        {
            if (ISPIXELFORMAT_PACKED(format))
            {
                PackedOrder PackedOrder = (PackedOrder)PIXELORDER(format);
                if (PackedOrder != PackedOrder.PACKEDORDER_ARGB && PackedOrder != PackedOrder.PACKEDORDER_RGBA && PackedOrder != PackedOrder.PACKEDORDER_ABGR)
                {
                    return PackedOrder == PackedOrder.PACKEDORDER_BGRA;
                }

                return true;
            }

            if (ISPIXELFORMAT_ARRAY(format))
            {
                ArrayOrder ArrayOrder = (ArrayOrder)PIXELORDER(format);
                if (ArrayOrder != ArrayOrder.ARRAYORDER_ARGB && ArrayOrder != ArrayOrder.ARRAYORDER_RGBA && ArrayOrder != ArrayOrder.ARRAYORDER_ABGR)
                {
                    return ArrayOrder == ArrayOrder.ARRAYORDER_BGRA;
                }

                return true;
            }

            return false;
        }

        static public bool ISPIXELFORMAT_FOURCC(uint format)
        {
            if (format == 0)
            {
                return PIXELFLAG(format) != 1;
            }

            return false;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AllocFormat")]
        static public extern IntPtr AllocFormat(uint pixel_format);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AllocPalette")]
        static public extern IntPtr AllocPalette(int ncolors);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CalculateGammaRamp")]
        static public extern void CalculateGammaRamp(float gamma, [Out][MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)] ushort[] ramp);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeFormat")]
        static public extern void FreeFormat(IntPtr format);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreePalette")]
        static public extern void FreePalette(IntPtr palette);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPixelFormatName")]
        private static extern IntPtr INTERNAL_GetPixelFormatName(uint format);

        static public string GetPixelFormatName(uint format)
        {
            return UTF8_ToManaged(INTERNAL_GetPixelFormatName(format));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRGB")]
        static public extern void GetRGB(uint pixel, IntPtr format, out byte r, out byte g, out byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRGBA")]
        static public extern void GetRGBA(uint pixel, IntPtr format, out byte r, out byte g, out byte b, out byte a);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MapRGB")]
        static public extern uint MapRGB(IntPtr format, byte r, byte g, byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MapRGBA")]
        static public extern uint MapRGBA(IntPtr format, byte r, byte g, byte b, byte a);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MasksToPixelFormatEnum")]
        static public extern uint MasksToPixelFormatEnum(int bpp, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PixelFormatEnumToMasks")]
        static public extern SDL_bool PixelFormatEnumToMasks(uint format, out int bpp, out uint Rmask, out uint Gmask, out uint Bmask, out uint Amask);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetPaletteColors")]
        static public extern int SetPaletteColors(IntPtr palette, [In] Color[] colors, int firstcolor, int ncolors);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetPixelFormatPalette")]
        static public extern int SetPixelFormatPalette(IntPtr format, IntPtr palette);

        static public SDL_bool PointInRect(ref Point p, ref Rect r)
        {
            if (p.x < r.x || p.x >= r.x + r.w || p.y < r.y || p.y >= r.y + r.h)
            {
                return SDL_bool.FALSE;
            }

            return SDL_bool.TRUE;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_EnclosePoints")]
        static public extern SDL_bool EnclosePoints([In] Point[] points, int count, ref Rect clip, out Rect result);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasIntersection")]
        static public extern SDL_bool HasIntersection(ref Rect A, ref Rect B);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IntersectRect")]
        static public extern SDL_bool IntersectRect(ref Rect A, ref Rect B, out Rect result);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IntersectRectAndLine")]
        static public extern SDL_bool IntersectRectAndLine(ref Rect rect, ref int X1, ref int Y1, ref int X2, ref int Y2);

        static public SDL_bool RectEmpty(ref Rect r)
        {
            if (r.w > 0 && r.h > 0)
            {
                return SDL_bool.FALSE;
            }

            return SDL_bool.TRUE;
        }

        static public SDL_bool RectEquals(ref Rect a, ref Rect b)
        {
            if (a.x != b.x || a.y != b.y || a.w != b.w || a.h != b.h)
            {
                return SDL_bool.FALSE;
            }

            return SDL_bool.TRUE;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnionRect")]
        static public extern void UnionRect(ref Rect A, ref Rect B, out Rect result);

        static public bool MUSTLOCK(IntPtr surface)
        {
            return (((Surface)Marshal.PtrToStructure(surface, typeof(Surface))).flags & 2) != 0;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlit")]
        static public extern int BlitSurface(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlit")]
        static public extern int BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlit")]
        static public extern int BlitSurface(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlit")]
        static public extern int BlitSurface(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlitScaled")]
        static public extern int BlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlitScaled")]
        static public extern int BlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlitScaled")]
        static public extern int BlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlitScaled")]
        static public extern int BlitScaled(IntPtr src, IntPtr srcrect, IntPtr dst, IntPtr dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ConvertPixels")]
        static public extern int ConvertPixels(int width, int height, uint src_format, IntPtr src, int src_pitch, uint dst_format, IntPtr dst, int dst_pitch);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ConvertSurface")]
        static public extern IntPtr ConvertSurface(IntPtr src, IntPtr fmt, uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ConvertSurfaceFormat")]
        static public extern IntPtr ConvertSurfaceFormat(IntPtr src, uint pixel_format, uint flags);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateRGBSurface")]
        static public extern IntPtr CreateRGBSurface(uint flags, int width, int height, int depth, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
        static public extern IntPtr CreateRGBSurfaceFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint Rmask, uint Gmask, uint Bmask, uint Amask);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateRGBSurfaceWithFormat")]
        static public extern IntPtr CreateRGBSurfaceWithFormat(uint flags, int width, int height, int depth, uint format);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateRGBSurfaceWithFormatFrom")]
        static public extern IntPtr CreateRGBSurfaceWithFormatFrom(IntPtr pixels, int width, int height, int depth, int pitch, uint format);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FillRect")]
        static public extern int FillRect(IntPtr dst, ref Rect rect, uint color);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FillRect")]
        static public extern int FillRect(IntPtr dst, IntPtr rect, uint color);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FillRects")]
        static public extern int FillRects(IntPtr dst, [In] Rect[] rects, int count, uint color);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeSurface")]
        static public extern void FreeSurface(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetClipRect")]
        static public extern void GetClipRect(IntPtr surface, out Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasColorKey")]
        static public extern SDL_bool HasColorKey(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetColorKey")]
        static public extern int GetColorKey(IntPtr surface, out uint key);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetSurfaceAlphaMod")]
        static public extern int GetSurfaceAlphaMod(IntPtr surface, out byte alpha);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetSurfaceBlendMode")]
        static public extern int GetSurfaceBlendMode(IntPtr surface, out BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetSurfaceColorMod")]
        static public extern int GetSurfaceColorMod(IntPtr surface, out byte r, out byte g, out byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LoadBMP_RW")]
        private static extern IntPtr INTERNAL_LoadBMP_RW(IntPtr src, int freesrc);

        static public IntPtr LoadBMP(string file)
        {
            return INTERNAL_LoadBMP_RW(RWFromFile(file, "rb"), 1);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockSurface")]
        static public extern int LockSurface(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LowerBlit")]
        static public extern int LowerBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LowerBlitScaled")]
        static public extern int LowerBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SaveBMP_RW")]
        private static extern int INTERNAL_SaveBMP_RW(IntPtr surface, IntPtr src, int freesrc);

        static public int SaveBMP(IntPtr surface, string file)
        {
            IntPtr src = RWFromFile(file, "wb");
            return INTERNAL_SaveBMP_RW(surface, src, 1);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetClipRect")]
        static public extern SDL_bool SetClipRect(IntPtr surface, ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetColorKey")]
        static public extern int SetColorKey(IntPtr surface, int flag, uint key);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetSurfaceAlphaMod")]
        static public extern int SetSurfaceAlphaMod(IntPtr surface, byte alpha);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetSurfaceBlendMode")]
        static public extern int SetSurfaceBlendMode(IntPtr surface, BlendMode blendMode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetSurfaceColorMod")]
        static public extern int SetSurfaceColorMod(IntPtr surface, byte r, byte g, byte b);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetSurfacePalette")]
        static public extern int SetSurfacePalette(IntPtr surface, IntPtr palette);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetSurfaceRLE")]
        static public extern int SetSurfaceRLE(IntPtr surface, int flag);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSurfaceRLE")]
        static public extern SDL_bool HasSurfaceRLE(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SoftStretch")]
        static public extern int SoftStretch(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockSurface")]
        static public extern void UnlockSurface(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlit")]
        static public extern int UpperBlit(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UpperBlitScaled")]
        static public extern int UpperBlitScaled(IntPtr src, ref Rect srcrect, IntPtr dst, ref Rect dstrect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DuplicateSurface")]
        static public extern IntPtr DuplicateSurface(IntPtr surface);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasClipboardText")]
        static public extern SDL_bool HasClipboardText();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetClipboardText")]
        private static extern IntPtr INTERNAL_GetClipboardText();

        static public string GetClipboardText()
        {
            return UTF8_ToManaged(INTERNAL_GetClipboardText(), freePtr: true);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetClipboardText")]
        private unsafe static extern int INTERNAL_SetClipboardText(byte* text);

        public unsafe static int SetClipboardText(string text)
        {
            byte* intPtr = Utf8Encode(text);
            int result = INTERNAL_SetClipboardText(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PumpEvents")]
        static public extern void PumpEvents();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PeepEvents")]
        static public extern int PeepEvents([Out] Event[] events, int numevents, eventaction action, EventType minType, EventType maxType);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasEvent")]
        static public extern SDL_bool HasEvent(EventType type);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasEvents")]
        static public extern SDL_bool HasEvents(EventType minType, EventType maxType);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FlushEvent")]
        static public extern void FlushEvent(EventType type);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FlushEvents")]
        static public extern void FlushEvents(EventType min, EventType max);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PollEvent")]
        static public extern int PollEvent(out Event _event);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WaitEvent")]
        static public extern int WaitEvent(out Event _event);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WaitEventTimeout")]
        static public extern int WaitEventTimeout(out Event _event, int timeout);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PushEvent")]
        static public extern int PushEvent(ref Event _event);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetEventFilter")]
        static public extern void SetEventFilter(EventFilter filter, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetEventFilter")]
        private static extern SDL_bool GetEventFilter(out IntPtr filter, out IntPtr userdata);

        static public SDL_bool GetEventFilter(out EventFilter filter, out IntPtr userdata)
        {
            IntPtr filter2 = IntPtr.Zero;
            SDL_bool result = GetEventFilter(out filter2, out userdata);
            if (filter2 != IntPtr.Zero)
            {
                filter = (EventFilter)Marshal.GetDelegateForFunctionPointer(filter2, typeof(EventFilter));
                return result;
            }

            filter = null;
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AddEventWatch")]
        static public extern void AddEventWatch(EventFilter filter, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DelEventWatch")]
        static public extern void DelEventWatch(EventFilter filter, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FilterEvents")]
        static public extern void FilterEvents(EventFilter filter, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_EventState")]
        static public extern byte EventState(EventType type, int state);

        static public byte GetEventState(EventType type)
        {
            return EventState(type, -1);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RegisterEvents")]
        static public extern uint RegisterEvents(int numevents);

        static public Keycode SCANCODE_TO_KEYCODE(Scancode X)
        {
            return (Keycode)(X | (Scancode)1073741824);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetKeyboardFocus")]
        static public extern IntPtr GetKeyboardFocus();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetKeyboardState")]
        static public extern IntPtr GetKeyboardState(out int numkeys);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetModState")]
        static public extern Keymod GetModState();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetModState")]
        static public extern void SetModState(Keymod modstate);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetKeyFromScancode")]
        static public extern Keycode GetKeyFromScancode(Scancode scancode);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetScancodeFromKey")]
        static public extern Scancode GetScancodeFromKey(Keycode key);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetScancodeName")]
        private static extern IntPtr INTERNAL_GetScancodeName(Scancode scancode);

        static public string GetScancodeName(Scancode scancode)
        {
            return UTF8_ToManaged(INTERNAL_GetScancodeName(scancode));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetScancodeFromName")]
        private unsafe static extern Scancode INTERNAL_GetScancodeFromName(byte* name);

        public unsafe static Scancode GetScancodeFromName(string name)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GetScancodeFromName(Utf8Encode(name, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetKeyName")]
        private static extern IntPtr INTERNAL_GetKeyName(Keycode key);

        static public string GetKeyName(Keycode key)
        {
            return UTF8_ToManaged(INTERNAL_GetKeyName(key));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetKeyFromName")]
        private unsafe static extern Keycode INTERNAL_GetKeyFromName(byte* name);

        public unsafe static Keycode GetKeyFromName(string name)
        {
            int num = Utf8Size(name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GetKeyFromName(Utf8Encode(name, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_StartTextInput")]
        static public extern void StartTextInput();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsTextInputActive")]
        static public extern SDL_bool IsTextInputActive();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_StopTextInput")]
        static public extern void StopTextInput();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetTextInputRect")]
        static public extern void SetTextInputRect(ref Rect rect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasScreenKeyboardSupport")]
        static public extern SDL_bool HasScreenKeyboardSupport();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsScreenKeyboardShown")]
        static public extern SDL_bool IsScreenKeyboardShown(IntPtr window);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetMouseFocus")]
        static public extern IntPtr GetMouseFocus();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetMouseState")]
        static public extern uint GetMouseState(out int x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetMouseState")]
        static public extern uint GetMouseState(IntPtr x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetMouseState")]
        static public extern uint GetMouseState(out int x, IntPtr y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetMouseState")]
        static public extern uint GetMouseState(IntPtr x, IntPtr y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetGlobalMouseState")]
        static public extern uint GetGlobalMouseState(out int x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetGlobalMouseState")]
        static public extern uint GetGlobalMouseState(IntPtr x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetGlobalMouseState")]
        static public extern uint GetGlobalMouseState(out int x, IntPtr y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetGlobalMouseState")]
        static public extern uint GetGlobalMouseState(IntPtr x, IntPtr y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRelativeMouseState")]
        static public extern uint GetRelativeMouseState(out int x, out int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WarpMouseInWindow")]
        static public extern void WarpMouseInWindow(IntPtr window, int x, int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WarpMouseGlobal")]
        static public extern int WarpMouseGlobal(int x, int y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetRelativeMouseMode")]
        static public extern int SetRelativeMouseMode(SDL_bool enabled);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CaptureMouse")]
        static public extern int CaptureMouse(SDL_bool enabled);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetRelativeMouseMode")]
        static public extern SDL_bool GetRelativeMouseMode();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateCursor")]
        static public extern IntPtr CreateCursor(IntPtr data, IntPtr mask, int w, int h, int hot_x, int hot_y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateColorCursor")]
        static public extern IntPtr CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CreateSystemCursor")]
        static public extern IntPtr CreateSystemCursor(SystemCursor id);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetCursor")]
        static public extern void SetCursor(IntPtr cursor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCursor")]
        static public extern IntPtr GetCursor();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeCursor")]
        static public extern void FreeCursor(IntPtr cursor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ShowCursor")]
        static public extern int ShowCursor(int toggle);

        static public uint BUTTON(uint X)
        {
            return (uint)(1 << (int)(X - 1));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumTouchDevices")]
        static public extern int GetNumTouchDevices();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTouchDevice")]
        static public extern long GetTouchDevice(int index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumTouchFingers")]
        static public extern int GetNumTouchFingers(long touchID);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTouchFinger")]
        static public extern IntPtr GetTouchFinger(long touchID, int index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTouchDeviceType")]
        static public extern TouchDeviceType GetTouchDeviceType(long touchID);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickRumble")]
        static public extern int JoystickRumble(IntPtr joystick, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickRumbleTriggers")]
        static public extern int JoystickRumbleTriggers(IntPtr joystick, ushort left_rumble, ushort right_rumble, uint duration_ms);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickClose")]
        static public extern void JoystickClose(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickEventState")]
        static public extern int JoystickEventState(int state);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetAxis")]
        static public extern short JoystickGetAxis(IntPtr joystick, int axis);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetAxisInitialState")]
        static public extern SDL_bool JoystickGetAxisInitialState(IntPtr joystick, int axis, out ushort state);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetBall")]
        static public extern int JoystickGetBall(IntPtr joystick, int ball, out int dx, out int dy);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetButton")]
        static public extern byte JoystickGetButton(IntPtr joystick, int button);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetHat")]
        static public extern byte JoystickGetHat(IntPtr joystick, int hat);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickName")]
        private static extern IntPtr INTERNAL_JoystickName(IntPtr joystick);

        static public string JoystickName(IntPtr joystick)
        {
            return UTF8_ToManaged(INTERNAL_JoystickName(joystick));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickNameForIndex")]
        private static extern IntPtr INTERNAL_JoystickNameForIndex(int device_index);

        static public string JoystickNameForIndex(int device_index)
        {
            return UTF8_ToManaged(INTERNAL_JoystickNameForIndex(device_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickNumAxes")]
        static public extern int JoystickNumAxes(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickNumBalls")]
        static public extern int JoystickNumBalls(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickNumButtons")]
        static public extern int JoystickNumButtons(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickNumHats")]
        static public extern int JoystickNumHats(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickOpen")]
        static public extern IntPtr JoystickOpen(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickUpdate")]
        static public extern void JoystickUpdate();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_NumJoysticks")]
        static public extern int NumJoysticks();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceGUID")]
        static public extern Guid JoystickGetDeviceGUID(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetGUID")]
        static public extern Guid JoystickGetGUID(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetGUIDString")]
        static public extern void JoystickGetGUIDString(Guid guid, byte[] pszGUID, int cbGUID);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetGUIDFromString")]
        private unsafe static extern Guid INTERNAL_JoystickGetGUIDFromString(byte* pchGUID);

        public unsafe static Guid JoystickGetGUIDFromString(string pchGuid)
        {
            int num = Utf8Size(pchGuid);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_JoystickGetGUIDFromString(Utf8Encode(pchGuid, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceVendor")]
        static public extern ushort JoystickGetDeviceVendor(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceProduct")]
        static public extern ushort JoystickGetDeviceProduct(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceProductVersion")]
        static public extern ushort JoystickGetDeviceProductVersion(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceType")]
        static public extern JoystickType JoystickGetDeviceType(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetDeviceInstanceID")]
        static public extern int JoystickGetDeviceInstanceID(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetVendor")]
        static public extern ushort JoystickGetVendor(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetProduct")]
        static public extern ushort JoystickGetProduct(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetProductVersion")]
        static public extern ushort JoystickGetProductVersion(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetSerial")]
        private static extern IntPtr INTERNAL_JoystickGetSerial(IntPtr joystick);

        static public string JoystickGetSerial(IntPtr joystick)
        {
            return UTF8_ToManaged(INTERNAL_JoystickGetSerial(joystick));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetType")]
        static public extern JoystickType JoystickGetType(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickGetAttached")]
        static public extern SDL_bool JoystickGetAttached(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickInstanceID")]
        static public extern int JoystickInstanceID(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickCurrentPowerLevel")]
        static public extern JoystickPowerLevel JoystickCurrentPowerLevel(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickFromInstanceID")]
        static public extern IntPtr JoystickFromInstanceID(int instance_id);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockJoysticks")]
        static public extern void LockJoysticks();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockJoysticks")]
        static public extern void UnlockJoysticks();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickFromPlayerIndex")]
        static public extern IntPtr JoystickFromPlayerIndex(int player_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickSetPlayerIndex")]
        static public extern void JoystickSetPlayerIndex(IntPtr joystick, int player_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickAttachVirtual")]
        static public extern int JoystickAttachVirtual(int type, int naxes, int nbuttons, int nhats);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickDetachVirtual")]
        static public extern int JoystickDetachVirtual(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickIsVirtual")]
        static public extern SDL_bool JoystickIsVirtual(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickSetVirtualAxis")]
        static public extern int JoystickSetVirtualAxis(IntPtr joystick, int axis, short value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickSetVirtualButton")]
        static public extern int JoystickSetVirtualButton(IntPtr joystick, int button, byte value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickSetVirtualHat")]
        static public extern int JoystickSetVirtualHat(IntPtr joystick, int hat, byte value);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickHasLED")]
        static public extern SDL_bool JoystickHasLED(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickSetLED")]
        static public extern int JoystickSetLED(IntPtr joystick, byte red, byte green, byte blue);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerAddMapping")]
        private unsafe static extern int INTERNAL_GameControllerAddMapping(byte* mappingString);

        public unsafe static int GameControllerAddMapping(string mappingString)
        {
            byte* intPtr = Utf8Encode(mappingString);
            int result = INTERNAL_GameControllerAddMapping(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerNumMappings")]
        static public extern int GameControllerNumMappings();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerMappingForIndex")]
        private static extern IntPtr INTERNAL_GameControllerMappingForIndex(int mapping_index);

        static public string GameControllerMappingForIndex(int mapping_index)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerMappingForIndex(mapping_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerAddMappingsFromRW")]
        private static extern int INTERNAL_GameControllerAddMappingsFromRW(IntPtr rw, int freerw);

        static public int GameControllerAddMappingsFromFile(string file)
        {
            return INTERNAL_GameControllerAddMappingsFromRW(RWFromFile(file, "rb"), 1);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerMappingForGUID")]
        private static extern IntPtr INTERNAL_GameControllerMappingForGUID(Guid guid);

        static public string GameControllerMappingForGUID(Guid guid)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerMappingForGUID(guid));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerMapping")]
        private static extern IntPtr INTERNAL_GameControllerMapping(IntPtr gamecontroller);

        static public string GameControllerMapping(IntPtr gamecontroller)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerMapping(gamecontroller));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsGameController")]
        static public extern SDL_bool IsGameController(int joystick_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerNameForIndex")]
        private static extern IntPtr INTERNAL_GameControllerNameForIndex(int joystick_index);

        static public string GameControllerNameForIndex(int joystick_index)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerNameForIndex(joystick_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerMappingForDeviceIndex")]
        private static extern IntPtr INTERNAL_GameControllerMappingForDeviceIndex(int joystick_index);

        static public string GameControllerMappingForDeviceIndex(int joystick_index)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerMappingForDeviceIndex(joystick_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerOpen")]
        static public extern IntPtr GameControllerOpen(int joystick_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerName")]
        private static extern IntPtr INTERNAL_GameControllerName(IntPtr gamecontroller);

        static public string GameControllerName(IntPtr gamecontroller)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerName(gamecontroller));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetVendor")]
        static public extern ushort GameControllerGetVendor(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetProduct")]
        static public extern ushort GameControllerGetProduct(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetProductVersion")]
        static public extern ushort GameControllerGetProductVersion(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetSerial")]
        private static extern IntPtr INTERNAL_GameControllerGetSerial(IntPtr gamecontroller);

        static public string GameControllerGetSerial(IntPtr gamecontroller)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerGetSerial(gamecontroller));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetAttached")]
        static public extern SDL_bool GameControllerGetAttached(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetJoystick")]
        static public extern IntPtr GameControllerGetJoystick(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerEventState")]
        static public extern int GameControllerEventState(int state);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerUpdate")]
        static public extern void GameControllerUpdate();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetAxisFromString")]
        private unsafe static extern GameControllerAxis INTERNAL_GameControllerGetAxisFromString(byte* pchString);

        public unsafe static GameControllerAxis GameControllerGetAxisFromString(string pchString)
        {
            int num = Utf8Size(pchString);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GameControllerGetAxisFromString(Utf8Encode(pchString, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetStringForAxis")]
        private static extern IntPtr INTERNAL_GameControllerGetStringForAxis(GameControllerAxis axis);

        static public string GameControllerGetStringForAxis(GameControllerAxis axis)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerGetStringForAxis(axis));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetBindForAxis")]
        private static extern INTERNAL_GameControllerButtonBind INTERNAL_GameControllerGetBindForAxis(IntPtr gamecontroller, GameControllerAxis axis);

        static public GameControllerButtonBind GameControllerGetBindForAxis(IntPtr gamecontroller, GameControllerAxis axis)
        {
            INTERNAL_GameControllerButtonBind iNTERNAL_GameControllerButtonBind = INTERNAL_GameControllerGetBindForAxis(gamecontroller, axis);
            GameControllerButtonBind result = default(GameControllerButtonBind);
            result.bindType = (GameControllerBindType)iNTERNAL_GameControllerButtonBind.bindType;
            result.value.hat.hat = iNTERNAL_GameControllerButtonBind.unionVal0;
            result.value.hat.hat_mask = iNTERNAL_GameControllerButtonBind.unionVal1;
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetAxis")]
        static public extern short GameControllerGetAxis(IntPtr gamecontroller, GameControllerAxis axis);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetButtonFromString")]
        private unsafe static extern GameControllerButton INTERNAL_GameControllerGetButtonFromString(byte* pchString);

        public unsafe static GameControllerButton GameControllerGetButtonFromString(string pchString)
        {
            int num = Utf8Size(pchString);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_GameControllerGetButtonFromString(Utf8Encode(pchString, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetStringForButton")]
        private static extern IntPtr INTERNAL_GameControllerGetStringForButton(GameControllerButton button);

        static public string GameControllerGetStringForButton(GameControllerButton button)
        {
            return UTF8_ToManaged(INTERNAL_GameControllerGetStringForButton(button));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetBindForButton")]
        private static extern INTERNAL_GameControllerButtonBind INTERNAL_GameControllerGetBindForButton(IntPtr gamecontroller, GameControllerButton button);

        static public GameControllerButtonBind GameControllerGetBindForButton(IntPtr gamecontroller, GameControllerButton button)
        {
            INTERNAL_GameControllerButtonBind iNTERNAL_GameControllerButtonBind = INTERNAL_GameControllerGetBindForButton(gamecontroller, button);
            GameControllerButtonBind result = default(GameControllerButtonBind);
            result.bindType = (GameControllerBindType)iNTERNAL_GameControllerButtonBind.bindType;
            result.value.hat.hat = iNTERNAL_GameControllerButtonBind.unionVal0;
            result.value.hat.hat_mask = iNTERNAL_GameControllerButtonBind.unionVal1;
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetButton")]
        static public extern byte GameControllerGetButton(IntPtr gamecontroller, GameControllerButton button);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerRumble")]
        static public extern int GameControllerRumble(IntPtr gamecontroller, ushort low_frequency_rumble, ushort high_frequency_rumble, uint duration_ms);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerRumbleTriggers")]
        static public extern int GameControllerRumbleTriggers(IntPtr gamecontroller, ushort left_rumble, ushort right_rumble, uint duration_ms);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerClose")]
        static public extern void GameControllerClose(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerFromInstanceID")]
        static public extern IntPtr GameControllerFromInstanceID(int joyid);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerTypeForIndex")]
        static public extern GameControllerType GameControllerTypeForIndex(int joystick_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetType")]
        static public extern GameControllerType GameControllerGetType(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerFromPlayerIndex")]
        static public extern IntPtr GameControllerFromPlayerIndex(int player_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerSetPlayerIndex")]
        static public extern void GameControllerSetPlayerIndex(IntPtr gamecontroller, int player_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerHasLED")]
        static public extern SDL_bool GameControllerHasLED(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerSetLED")]
        static public extern int GameControllerSetLED(IntPtr gamecontroller, byte red, byte green, byte blue);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerHasAxis")]
        static public extern SDL_bool GameControllerHasAxis(IntPtr gamecontroller, GameControllerAxis axis);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerHasButton")]
        static public extern SDL_bool GameControllerHasButton(IntPtr gamecontroller, GameControllerButton button);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetNumTouchpads")]
        static public extern int GameControllerGetNumTouchpads(IntPtr gamecontroller);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetNumTouchpadFingers")]
        static public extern int GameControllerGetNumTouchpadFingers(IntPtr gamecontroller, int touchpad);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetTouchpadFinger")]
        static public extern int GameControllerGetTouchpadFinger(IntPtr gamecontroller, int touchpad, int finger, out byte state, out float x, out float y, out float pressure);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerHasSensor")]
        static public extern SDL_bool GameControllerHasSensor(IntPtr gamecontroller, SensorType type);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerSetSensorEnabled")]
        static public extern int GameControllerSetSensorEnabled(IntPtr gamecontroller, SensorType type, SDL_bool enabled);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerIsSensorEnabled")]
        static public extern SDL_bool GameControllerIsSensorEnabled(IntPtr gamecontroller, SensorType type);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GameControllerGetSensorData")]
        static public extern int GameControllerGetSensorData(IntPtr gamecontroller, SensorType type, IntPtr data, int num_values);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticClose")]
        static public extern void HapticClose(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticDestroyEffect")]
        static public extern void HapticDestroyEffect(IntPtr haptic, int effect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticEffectSupported")]
        static public extern int HapticEffectSupported(IntPtr haptic, ref HapticEffect effect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticGetEffectStatus")]
        static public extern int HapticGetEffectStatus(IntPtr haptic, int effect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticIndex")]
        static public extern int HapticIndex(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticName")]
        private static extern IntPtr INTERNAL_HapticName(int device_index);

        static public string HapticName(int device_index)
        {
            return UTF8_ToManaged(INTERNAL_HapticName(device_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticNewEffect")]
        static public extern int HapticNewEffect(IntPtr haptic, ref HapticEffect effect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticNumAxes")]
        static public extern int HapticNumAxes(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticNumEffects")]
        static public extern int HapticNumEffects(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticNumEffectsPlaying")]
        static public extern int HapticNumEffectsPlaying(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticOpen")]
        static public extern IntPtr HapticOpen(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticOpened")]
        static public extern int HapticOpened(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticOpenFromJoystick")]
        static public extern IntPtr HapticOpenFromJoystick(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticOpenFromMouse")]
        static public extern IntPtr HapticOpenFromMouse();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticPause")]
        static public extern int HapticPause(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticQuery")]
        static public extern uint HapticQuery(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticRumbleInit")]
        static public extern int HapticRumbleInit(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticRumblePlay")]
        static public extern int HapticRumblePlay(IntPtr haptic, float strength, uint length);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticRumbleStop")]
        static public extern int HapticRumbleStop(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticRumbleSupported")]
        static public extern int HapticRumbleSupported(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticRunEffect")]
        static public extern int HapticRunEffect(IntPtr haptic, int effect, uint iterations);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticSetAutocenter")]
        static public extern int HapticSetAutocenter(IntPtr haptic, int autocenter);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticSetGain")]
        static public extern int HapticSetGain(IntPtr haptic, int gain);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticStopAll")]
        static public extern int HapticStopAll(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticStopEffect")]
        static public extern int HapticStopEffect(IntPtr haptic, int effect);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticUnpause")]
        static public extern int HapticUnpause(IntPtr haptic);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HapticUpdateEffect")]
        static public extern int HapticUpdateEffect(IntPtr haptic, int effect, ref HapticEffect data);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_JoystickIsHaptic")]
        static public extern int JoystickIsHaptic(IntPtr joystick);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MouseIsHaptic")]
        static public extern int MouseIsHaptic();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_NumHaptics")]
        static public extern int NumHaptics();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_NumSensors")]
        static public extern int NumSensors();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetDeviceName")]
        private static extern IntPtr INTERNAL_SensorGetDeviceName(int device_index);

        static public string SensorGetDeviceName(int device_index)
        {
            return UTF8_ToManaged(INTERNAL_SensorGetDeviceName(device_index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetDeviceType")]
        static public extern SensorType SensorGetDeviceType(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetDeviceNonPortableType")]
        static public extern int SensorGetDeviceNonPortableType(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetDeviceInstanceID")]
        static public extern int SensorGetDeviceInstanceID(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorOpen")]
        static public extern IntPtr SensorOpen(int device_index);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorFromInstanceID")]
        static public extern IntPtr SensorFromInstanceID(int instance_id);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetName")]
        private static extern IntPtr INTERNAL_SensorGetName(IntPtr sensor);

        static public string SensorGetName(IntPtr sensor)
        {
            return UTF8_ToManaged(INTERNAL_SensorGetName(sensor));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetType")]
        static public extern SensorType SensorGetType(IntPtr sensor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetNonPortableType")]
        static public extern int SensorGetNonPortableType(IntPtr sensor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetInstanceID")]
        static public extern int SensorGetInstanceID(IntPtr sensor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorGetData")]
        static public extern int SensorGetData(IntPtr sensor, float[] data, int num_values);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorClose")]
        static public extern void SensorClose(IntPtr sensor);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SensorUpdate")]
        static public extern void SensorUpdate();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockSensors")]
        static public extern void LockSensors();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockSensors")]
        static public extern void UnlockSensors();

        static public ushort AUDIO_BITSIZE(ushort x)
        {
            return (ushort)(x & 0xFFu);
        }

        static public bool AUDIO_ISFLOAT(ushort x)
        {
            return (x & 0x100) != 0;
        }

        static public bool AUDIO_ISBIGENDIAN(ushort x)
        {
            return (x & 0x1000) != 0;
        }

        static public bool AUDIO_ISSIGNED(ushort x)
        {
            return (x & 0x8000) != 0;
        }

        static public bool AUDIO_ISINT(ushort x)
        {
            return (x & 0x100) == 0;
        }

        static public bool AUDIO_ISLITTLEENDIAN(ushort x)
        {
            return (x & 0x1000) == 0;
        }

        static public bool AUDIO_ISUNSIGNED(ushort x)
        {
            return (x & 0x8000) == 0;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioInit")]
        private unsafe static extern int INTERNAL_AudioInit(byte* driver_name);

        public unsafe static int AudioInit(string driver_name)
        {
            int num = Utf8Size(driver_name);
            byte* buffer = stackalloc byte[(int)(uint)num];
            return INTERNAL_AudioInit(Utf8Encode(driver_name, buffer, num));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioQuit")]
        static public extern void AudioQuit();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CloseAudio")]
        static public extern void CloseAudio();

        /// <summary>
        /// Use this function to shut down audio processing and close the audio device.
        /// </summary>
        /// <param name="dev">an audio device previously opened with <see cref="OpenAudioDevice"/></param>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_CloseAudioDevice")]
        static public extern void CloseAudioDevice(uint dev);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeWAV")]
        static public extern void FreeWAV(IntPtr audio_buf);

        /// <summary>
        /// Get the human-readable name of a specific audio device.
        /// </summary>
        /// <param name="index">the index of the audio device; valid values range from 0 to SDL_GetNumAudioDevices() - 1</param>
        /// <param name="iscapture">non-zero to query the list of recording devices, zero to query the list of output devices.</param>
        /// <returns>Returns the name of the audio device at the requested index, or NULL on error.</returns>
        /// <remarks>
        /// <para>This function is only valid after successfully initializing the audio subsystem. The values returned by this function reflect the latest call to SDL_GetNumAudioDevices(); re-call that function to redetect available hardware.</para>
        /// <para>The string returned by this function is UTF-8 encoded, read-only, and managed internally.You are not to free it. If you need to keep the string for any length of time, you should make your own copy of it, as it will be invalid next time any of several other SDL functions are called.</para>
        /// </remarks>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION)]
        static private extern IntPtr SDL_GetAudioDeviceName(int index, int iscapture);
        static public string GetAudioDeviceName(int index, int iscapture) => Marshal.PtrToStringUTF8(SDL_GetAudioDeviceName(index, iscapture)) ?? "";
        static public string GetAudioDeviceName(int index, bool iscapture) => GetAudioDeviceName(index, iscapture ? 1 : 0);

        /// <summary>
        /// Use this function to get the current audio state of an audio device.
        /// </summary>
        /// <param name="dev">the ID of an audio device previously opened with <see cref="OpenAudioDevice"/></param>
        /// <returns>Returns the SDL_AudioStatus of the specified audio device.</returns>
        /// <remarks>This function is available since SDL 2.0.0.</remarks>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetAudioDeviceStatus")]
        static public extern AudioStatus GetAudioDeviceStatus(uint dev);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetAudioDriver")]
        private static extern IntPtr INTERNAL_GetAudioDriver(int index);

        static public string GetAudioDriver(int index)
        {
            return UTF8_ToManaged(INTERNAL_GetAudioDriver(index));
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetAudioStatus")]
        static public extern AudioStatus GetAudioStatus();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCurrentAudioDriver")]
        private static extern IntPtr INTERNAL_GetCurrentAudioDriver();

        static public string GetCurrentAudioDriver()
        {
            return UTF8_ToManaged(INTERNAL_GetCurrentAudioDriver());
        }

        /// <summary>
        /// Get the number of built-in audio devices.
        /// </summary>
        /// <param name="iscapture">zero to request playback devices, non-zero to request recording devices</param>
        /// <returns>Returns the number of available devices exposed by the current driver or -1 if an explicit list of devices can't be determined. A return value of -1 does not necessarily mean an error condition.</returns>
        /// <remarks> 
        /// <para>This function is only valid after successfully initializing the audio subsystem.</para>
        /// <para>Note that audio capture support is not implemented as of SDL 2.0.4, so the iscapture parameter is for future expansion and should always be zero for now.</para>
        /// <para>This function will return -1 if an explicit list of devices can't be determined. Returning -1 is not an error. For example, if SDL is set up to talk to a remote audio server, it can't list every one available on the Internet, but it will still allow a specific host to be specified in <see cref="OpenAudioDevice"/>().</para>
        /// <para>In many common cases, when this function returns a value &lt;= 0, it can still successfully open the default device (NULL for first argument of <see cref="OpenAudioDevice"/>()).</para>
        /// <para>This function may trigger a complete redetect of available hardware.It should not be called for each iteration of a loop, but rather once at the start of a loop:</para>
        /// <code>
        /// // Don't do this:
        /// for (int i = 0; i &lt; <see cref="GetNumAudioDevices"/>(0); i++)
        /// // do this instead:
        /// const int count = <see cref="GetNumAudioDevices"/>(0);
        /// for (int i = 0; i &lt; count; ++i) 
        /// {
        ///     do_something_here();
        /// }
        /// </code>
        /// </remarks>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetNumAudioDevices")]
        static public extern int GetNumAudioDevices(int iscapture);
        static public int GetNumAudioDevices(bool iscapture) => GetNumAudioDevices(iscapture ? 1 : 0);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LoadWAV_RW")]
        private static extern IntPtr INTERNAL_LoadWAV_RW(IntPtr src, int freesrc, out AudioSpec spec, out IntPtr audio_buf, out uint audio_len);

        static public IntPtr LoadWAV(string file, out AudioSpec spec, out IntPtr audio_buf, out uint audio_len)
        {
            return INTERNAL_LoadWAV_RW(RWFromFile(file, "rb"), 1, out spec, out audio_buf, out audio_len);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockAudio")]
        static public extern void LockAudio();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_LockAudioDevice")]
        static public extern void LockAudioDevice(uint dev);

        [Obsolete($"Use {nameof(MixAudioFormat)} instead")]
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MixAudio")]
        static public extern void MixAudio([Out] byte* dst, [In] byte* src, uint len, int volume);

        /// <summary>
        /// Mix audio data in a specified format.
        /// </summary>
        /// <param name="dst">the destination for the mixed audio</param>
        /// <param name="src">the source audio buffer to be mixed</param>
        /// <param name="format">the <see cref="AudioFormat"/> structure representing the desired audio format</param>
        /// <param name="len">the length of the audio buffer in bytes</param>
        /// <param name="volume">ranges from 0 - 128, and should be set to <see cref="MIX_MAXVOLUME"/> for full audio volume</param>
        /// <remarks>
        /// <para>This takes an audio buffer src of len bytes of format data and mixes it into dst, performing addition, volume adjustment, and overflow clipping. The buffer pointed to by dst must also be len bytes of format data.</para>
        /// <para>This is provided for convenience -- you can mix your own audio data.</para>
        /// <para>Do not use this function for mixing together more than two streams of sample data.The output from repeated application of this function may be distorted by clipping, because there is no accumulator with greater range than the input (not to mention this being an inefficient way of doing it).</para>
        /// <para>It is a common misconception that this function is required to write audio data to an output stream in an audio callback. While you can do that, <see cref="MixAudioFormat"/> is really only needed when you're mixing a single audio stream with a volume adjustment.</para>
        /// </remarks>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_MixAudioFormat")]
        static public extern void MixAudioFormat([Out] byte* dst, [In] byte* src, [MarshalAs(UnmanagedType.U2)] AudioFormat format, uint len, int volume);
        [Obsolete($"Use {nameof(OpenAudioDevice)} instead")]
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_OpenAudio")]
        static public extern int OpenAudio(ref AudioSpec desired, out AudioSpec obtained);
        [Obsolete($"Use {nameof(OpenAudioDevice)} instead")]
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_OpenAudio")]
        static public extern int OpenAudio(ref AudioSpec desired, IntPtr obtained);
        [Flags]
        public enum AllowedChangeFlags
        {
            None = 0,
            FREQUENCY_CHANGE = 0x00000001,
            FORMAT_CHANGE = 0x00000002,
            CHANNELS_CHANGE = 0x00000004,
            ANY_CHANGE = (FREQUENCY_CHANGE | FORMAT_CHANGE | CHANNELS_CHANGE),
            All = ANY_CHANGE
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_OpenAudioDevice")]
        static public extern unsafe uint OpenAudioDevice([MarshalAs(UnmanagedType.LPUTF8Str)] string deviceName, int iscapture, [In] AudioSpec* desired, [Out] AudioSpec* obtained, [MarshalAs(UnmanagedType.I4)] AllowedChangeFlags allowed_changes);
        static public unsafe uint OpenAudioDevice(string deviceName, bool iscapture, [In] AudioSpec* desired, [Out] AudioSpec* obtained, AllowedChangeFlags allowed_changes) => OpenAudioDevice(deviceName, iscapture ? 1 : 0, desired, obtained, allowed_changes);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PauseAudio")]
        static public extern void PauseAudio(int pause_on);

        /// <summary>
        /// Use this function to pause and unpause audio playback on a specified device.
        /// </summary>
        /// <param name="dev">a device opened by <see cref="OpenAudioDevice"/></param>
        /// <param name="pause_on">non-zero to pause, 0 to unpause</param>
        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_PauseAudioDevice")]
        static public extern void PauseAudioDevice(uint dev, int pause_on);
        static public void PauseAudioDevice(uint deviceID, bool pause_on) => PauseAudioDevice(deviceID, pause_on ? 1 : 0);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockAudio")]
        static public extern void UnlockAudio();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_UnlockAudioDevice")]
        static public extern void UnlockAudioDevice(uint dev);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_QueueAudio")]
        static public extern int QueueAudio(uint dev, IntPtr data, uint len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_DequeueAudio")]
        static public extern uint DequeueAudio(uint dev, IntPtr data, uint len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetQueuedAudioSize")]
        static public extern uint GetQueuedAudioSize(uint dev);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_ClearQueuedAudio")]
        static public extern void ClearQueuedAudio(uint dev);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_NewAudioStream")]
        static public extern IntPtr NewAudioStream(ushort src_format, byte src_channels, int src_rate, ushort dst_format, byte dst_channels, int dst_rate);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioStreamPut")]
        static public extern int AudioStreamPut(IntPtr stream, IntPtr buf, int len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioStreamGet")]
        static public extern int AudioStreamGet(IntPtr stream, IntPtr buf, int len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioStreamAvailable")]
        static public extern int AudioStreamAvailable(IntPtr stream);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AudioStreamClear")]
        static public extern void AudioStreamClear(IntPtr stream);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_FreeAudioStream")]
        static public extern void FreeAudioStream(IntPtr stream);

        static public bool TICKS_PASSED(uint A, uint B)
        {
            return (int)(B - A) <= 0;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Delay")]
        static public extern void Delay(uint ms);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetTicks")]
        static public extern uint GetTicks();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPerformanceCounter")]
        static public extern ulong GetPerformanceCounter();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPerformanceFrequency")]
        static public extern ulong GetPerformanceFrequency();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AddTimer")]
        static public extern int AddTimer(uint interval, TimerCallback callback, IntPtr param);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_RemoveTimer")]
        static public extern SDL_bool RemoveTimer(int id);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SetWindowsMessageHook")]
        static public extern void SetWindowsMessageHook(WindowsMessageHook callback, IntPtr userdata);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_iPhoneSetAnimationCallback")]
        static public extern int iPhoneSetAnimationCallback(IntPtr window, int interval, iPhoneAnimationCallback callback, IntPtr callbackParam);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_iPhoneSetEventPump")]
        static public extern void iPhoneSetEventPump(SDL_bool enabled);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidGetJNIEnv")]
        static public extern IntPtr AndroidGetJNIEnv();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidGetActivity")]
        static public extern IntPtr AndroidGetActivity();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsAndroidTV")]
        static public extern SDL_bool IsAndroidTV();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsChromebook")]
        static public extern SDL_bool IsChromebook();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsDeXMode")]
        static public extern SDL_bool IsDeXMode();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidBackButton")]
        static public extern void AndroidBackButton();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidGetInternalStoragePath")]
        private static extern IntPtr INTERNAL_AndroidGetInternalStoragePath();

        static public string AndroidGetInternalStoragePath()
        {
            return UTF8_ToManaged(INTERNAL_AndroidGetInternalStoragePath());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidGetExternalStorageState")]
        static public extern int AndroidGetExternalStorageState();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidGetExternalStoragePath")]
        private static extern IntPtr INTERNAL_AndroidGetExternalStoragePath();

        static public string AndroidGetExternalStoragePath()
        {
            return UTF8_ToManaged(INTERNAL_AndroidGetExternalStoragePath());
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetAndroidSDKVersion")]
        static public extern int GetAndroidSDKVersion();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_AndroidRequestPermission")]
        public unsafe static extern SDL_bool INTERNAL_AndroidRequestPermission(byte* permission);

        public unsafe static SDL_bool AndroidRequestPermission(string permission)
        {
            byte* intPtr = Utf8Encode(permission);
            SDL_bool result = INTERNAL_AndroidRequestPermission(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_WinRTGetDeviceFamily")]
        static public extern WinRT_DeviceFamily WinRTGetDeviceFamily();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_IsTablet")]
        static public extern SDL_bool IsTablet();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetWindowWMInfo")]
        static public extern SDL_bool GetWindowWMInfo(IntPtr window, ref SysWMinfo info);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetBasePath")]
        private static extern IntPtr INTERNAL_GetBasePath();

        static public string GetBasePath()
        {
            return UTF8_ToManaged(INTERNAL_GetBasePath(), freePtr: true);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPrefPath")]
        private unsafe static extern IntPtr INTERNAL_GetPrefPath(byte* org, byte* app);

        public unsafe static string GetPrefPath(string org, string app)
        {
            int num = Utf8SizeNullable(org);
            byte* buffer = stackalloc byte[(int)(uint)num];
            int num2 = Utf8SizeNullable(app);
            byte* buffer2 = stackalloc byte[(int)(uint)num2];
            return UTF8_ToManaged(INTERNAL_GetPrefPath(Utf8EncodeNullable(org, buffer, num), Utf8EncodeNullable(app, buffer2, num2)), freePtr: true);
        }

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPowerInfo")]
        static public extern PowerState GetPowerInfo(out int secs, out int pct);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCPUCount")]
        static public extern int GetCPUCount();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetCPUCacheLineSize")]
        static public extern int GetCPUCacheLineSize();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasRDTSC")]
        static public extern SDL_bool HasRDTSC();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasAltiVec")]
        static public extern SDL_bool HasAltiVec();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasMMX")]
        static public extern SDL_bool HasMMX();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_Has3DNow")]
        static public extern SDL_bool Has3DNow();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSSE")]
        static public extern SDL_bool HasSSE();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSSE2")]
        static public extern SDL_bool HasSSE2();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSSE3")]
        static public extern SDL_bool HasSSE3();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSSE41")]
        static public extern SDL_bool HasSSE41();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasSSE42")]
        static public extern SDL_bool HasSSE42();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasAVX")]
        static public extern SDL_bool HasAVX();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasAVX2")]
        static public extern SDL_bool HasAVX2();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasAVX512F")]
        static public extern SDL_bool HasAVX512F();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasNEON")]
        static public extern SDL_bool HasNEON();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetSystemRAM")]
        static public extern int GetSystemRAM();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SIMDGetAlignment")]
        static public extern uint SIMDGetAlignment();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SIMDAlloc")]
        static public extern IntPtr SIMDAlloc(uint len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SIMDRealloc")]
        static public extern IntPtr SIMDRealloc(IntPtr ptr, uint len);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_SIMDFree")]
        static public extern void SIMDFree(IntPtr ptr);

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_HasARMSIMD")]
        static public extern void HasARMSIMD();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_GetPreferredLocales")]
        static public extern IntPtr GetPreferredLocales();

        [DllImport(LIBRARY_NAME, CallingConvention = CALLING_CONVENTION, EntryPoint = "SDL_OpenURL")]
        private unsafe static extern int INTERNAL_OpenURL(byte* url);

        public unsafe static int OpenURL(string url)
        {
            byte* intPtr = Utf8Encode(url);
            int result = INTERNAL_OpenURL(intPtr);
            Marshal.FreeHGlobal((IntPtr)intPtr);
            return result;
        }
    }
}