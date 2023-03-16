namespace SolutionBuilder.Model
{
    internal class Module
    {

    }

    public enum PlatformType : byte
    {
        Mixed = 0x0,
        AnyCpu = 0x1,
        Arm = 0x2,
        x64 = 0x3,
        x86 = 0x4,
    }

    public enum ProjectType : byte
    {
        Unknown = 0x0,
        CS = 0x1,
        VB = 0x2,
        VCX = 0x3,
    }
}
