// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Define.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace NFProto {

  /// <summary>Holder for reflection information generated from Define.proto</summary>
  public static partial class DefineReflection {

    #region Descriptor
    /// <summary>File descriptor for Define.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static DefineReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxEZWZpbmUucHJvdG8SB05GUHJvdG8aDEV4dGVuZC5wcm90bypuCg1Nc2dN",
            "YWluSWRFbnVtEgsKB0ludmFsaWQQABIRCghIZWF0QmVhdBABGgOISwASPQoI",
            "RGFpbHlBc2sQAhoviEsBkksTTkZQcm90by5DMlNEYWlseUFza5pLE05GUHJv",
            "dG8uUzJDRGFpbHlBc2sqHgoMTXNnU3ViSWRFbnVtEg4KCk5vU3BlY2lmaWMQ",
            "AGIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { global::NFProto.ExtendReflection.Descriptor, },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::NFProto.MsgMainIdEnum), typeof(global::NFProto.MsgSubIdEnum), }, null, null));
    }
    #endregion

  }
  #region Enums
  public enum MsgMainIdEnum {
    [pbr::OriginalName("Invalid")] Invalid = 0,
    [pbr::OriginalName("HeatBeat")] HeatBeat = 1,
    [pbr::OriginalName("DailyAsk")] DailyAsk = 2,
  }

  public enum MsgSubIdEnum {
    [pbr::OriginalName("NoSpecific")] NoSpecific = 0,
  }

  #endregion

}

#endregion Designer generated code
