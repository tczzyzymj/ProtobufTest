// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Extend.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from Extend.proto</summary>
public static partial class ExtendReflection {

  #region Descriptor
  /// <summary>File descriptor for Extend.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static ExtendReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgxFeHRlbmQucHJvdG8aIGdvb2dsZS9wcm90b2J1Zi9kZXNjcmlwdG9yLnBy",
          "b3RvOjkKDVNwZWNpZmljUHJvdG8SIS5nb29nbGUucHJvdG9idWYuRW51bVZh",
          "bHVlT3B0aW9ucxixCSABKAg6NwoLTmV0UmVxUHJvdG8SIS5nb29nbGUucHJv",
          "dG9idWYuRW51bVZhbHVlT3B0aW9ucxiyCSABKAk6NwoLTmV0UnNwUHJvdG8S",
          "IS5nb29nbGUucHJvdG9idWYuRW51bVZhbHVlT3B0aW9ucxizCSABKAliBnBy",
          "b3RvMw=="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { global::Google.Protobuf.Reflection.DescriptorReflection.Descriptor, },
        new pbr::GeneratedClrTypeInfo(null, new pb::Extension[] { ExtendExtensions.SpecificProto, ExtendExtensions.NetReqProto, ExtendExtensions.NetRspProto }, null));
  }
  #endregion

}
/// <summary>Holder for extension identifiers generated from the top level of Extend.proto</summary>
public static partial class ExtendExtensions {
  public static readonly pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, bool> SpecificProto =
    new pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, bool>(1201, pb::FieldCodec.ForBool(9608, false));
  public static readonly pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, string> NetReqProto =
    new pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, string>(1202, pb::FieldCodec.ForString(9618, ""));
  public static readonly pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, string> NetRspProto =
    new pb::Extension<global::Google.Protobuf.Reflection.EnumValueOptions, string>(1203, pb::FieldCodec.ForString(9626, ""));
}


#endregion Designer generated code
