using System.ComponentModel;

namespace MergeDiana.GameLib {
    public enum DianaStrawberryType {
        [Description("0")]
        None = 0b0000_0000_0000_0000, // 无，表示空
        [Description("1")]
        A = 0b0000_0000_0000_0001, // 1
        [Description("2")]
        B = 0b0000_0000_0000_0010, // 2
        [Description("4")]
        C = 0b0000_0000_0000_0100, // 4
        [Description("8")]
        D = 0b0000_0000_0000_1000, // 8
        [Description("16")]
        E = 0b0000_0000_0001_0000, // 16
        [Description("32")]
        F = 0b0000_0000_0010_0000, // 32
        [Description("64")]
        G = 0b0000_0000_0100_0000, // 64
        [Description("128")]
        H = 0b0000_0000_1000_0000, // 128
        [Description("256")]
        I = 0b0000_0001_0000_0000, // 256
        [Description("512")]
        J = 0b0000_0010_0000_0000, // 512
        [Description("1024")]
        K = 0b0000_0100_0000_0000, // 1024
        [Description("2048")]
        L = 0b0000_1000_0000_0000, // 2048
        [Description("4096")]
        M = 0b0001_0000_0000_0000, // 4096
        [Description("8192")]
        N = 0b0010_0000_0000_0000, // 8192
        [Description("16384")]
        O = 0b0100_0000_0000_0000, // 16384
        [Description("32768")]
        P = 0b1000_0000_0000_0000  // 32768
    }
}
