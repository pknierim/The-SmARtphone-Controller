// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: HoloLensAndroidMessaging.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Google.Protobuf.HoloLensAndroidMessaging {

  /// <summary>Holder for reflection information generated from HoloLensAndroidMessaging.proto</summary>
  public static partial class HoloLensAndroidMessagingReflection {

    #region Descriptor
    /// <summary>File descriptor for HoloLensAndroidMessaging.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static HoloLensAndroidMessagingReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5Ib2xvTGVuc0FuZHJvaWRNZXNzYWdpbmcucHJvdG8SAk1UIrcBChJPYmpl",
            "Y3RNYW5pcHVsYXRpb24SFAoMdHJhbnNsYXRpb25YGAEgASgCEhQKDHRyYW5z",
            "bGF0aW9uWRgCIAEoAhIUCgx0cmFuc2xhdGlvbloYAyABKAISDQoFc2NhbGUY",
            "BCABKAISEQoJcm90YXRpb25ZGAUgASgCEhQKDGlzRmlyc3RUb3VjaBgGIAEo",
            "BxITCgtpc1NlbGVjdGluZxgHIAEoBxISCgppc0ZpbmlzaGVkGAggASgHIoYC",
            "Cg5VSU1hbmlwdWxhdGlvbhITCgtpc1NlbGVjdGluZxgBIAEoBxIPCgdzY3Jv",
            "bGxYGAIgASgCEg8KB3Njcm9sbFkYAyABKAISGQoRaGFzQW5kcm9pZFVJSW5w",
            "dXQYBCABKAcSOQoOYW5kcm9pZFVJSW5wdXQYBSABKAsyIS5NVC5VSU1hbmlw",
            "dWxhdGlvbi5BbmRyb2lkVUlJbnB1dBISCgppc0ZpbmlzaGVkGAYgASgHGlMK",
            "DkFuZHJvaWRVSUlucHV0EhIKCm9iamVjdFR5cGUYASABKAcSDwoHcXVhbGl0",
            "eRgCIAEoBxINCgVzY2FsZRgDIAEoBxINCgVjb2xvchgEIAEoByLnAgoPSG9s",
            "b0xlbnNNZXNzYWdlEiwKB21lc3NhZ2UYASABKA4yGy5NVC5Ib2xvTGVuc01l",
            "c3NhZ2UuTWVzc2FnZSKlAgoHTWVzc2FnZRIeChpVU0VMRVNTX0NPTlNUQU5U",
            "X05FVkVSX1VTRRAAEh8KG1VTRV9DQVNFX09CSkVDVF9UUkFOU0xBVElPThAB",
            "EhIKDlVTRV9DQVNFX0NMRUFSEAISHAoYVVNFX0NBU0VfVUlfTUFOSVBVTEFU",
            "SU9OEAMSJwojVVNFX0NBU0VfVUlfTUFOSVBVTEFUSU9OX0FORFJPSURfVUkQ",
            "BBIgChxPQkpFQ1RfTUFOSVBVTEFUSU9OX0ZJTklTSEVEEAUSJAogT0JKRUNU",
            "X0ZPUl9NQU5JUFVMQVRJT05fU0VMRUNURUQQBhIbChdVSV9NQU5JUFVMQVRJ",
            "T05fQ09SUkVDVBAHEhkKFVVJX01BTklQVUxBVElPTl9GQUxTRRAIQiuqAihH",
            "b29nbGUuUHJvdG9idWYuSG9sb0xlbnNBbmRyb2lkTWVzc2FnaW5nYgZwcm90",
            "bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.HoloLensAndroidMessaging.ObjectManipulation), global::Google.Protobuf.HoloLensAndroidMessaging.ObjectManipulation.Parser, new[]{ "TranslationX", "TranslationY", "TranslationZ", "Scale", "RotationY", "IsFirstTouch", "IsSelecting", "IsFinished" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation), global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Parser, new[]{ "IsSelecting", "ScrollX", "ScrollY", "HasAndroidUIInput", "AndroidUIInput", "IsFinished" }, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput), global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput.Parser, new[]{ "ObjectType", "Quality", "Scale", "Color" }, null, null, null)}),
            new pbr::GeneratedClrTypeInfo(typeof(global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage), global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage.Parser, new[]{ "Message" }, null, new[]{ typeof(global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage.Types.Message) }, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ObjectManipulation : pb::IMessage<ObjectManipulation> {
    private static readonly pb::MessageParser<ObjectManipulation> _parser = new pb::MessageParser<ObjectManipulation>(() => new ObjectManipulation());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ObjectManipulation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensAndroidMessagingReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ObjectManipulation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ObjectManipulation(ObjectManipulation other) : this() {
      translationX_ = other.translationX_;
      translationY_ = other.translationY_;
      translationZ_ = other.translationZ_;
      scale_ = other.scale_;
      rotationY_ = other.rotationY_;
      isFirstTouch_ = other.isFirstTouch_;
      isSelecting_ = other.isSelecting_;
      isFinished_ = other.isFinished_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ObjectManipulation Clone() {
      return new ObjectManipulation(this);
    }

    /// <summary>Field number for the "translationX" field.</summary>
    public const int TranslationXFieldNumber = 1;
    private float translationX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float TranslationX {
      get { return translationX_; }
      set {
        translationX_ = value;
      }
    }

    /// <summary>Field number for the "translationY" field.</summary>
    public const int TranslationYFieldNumber = 2;
    private float translationY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float TranslationY {
      get { return translationY_; }
      set {
        translationY_ = value;
      }
    }

    /// <summary>Field number for the "translationZ" field.</summary>
    public const int TranslationZFieldNumber = 3;
    private float translationZ_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float TranslationZ {
      get { return translationZ_; }
      set {
        translationZ_ = value;
      }
    }

    /// <summary>Field number for the "scale" field.</summary>
    public const int ScaleFieldNumber = 4;
    private float scale_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float Scale {
      get { return scale_; }
      set {
        scale_ = value;
      }
    }

    /// <summary>Field number for the "rotationY" field.</summary>
    public const int RotationYFieldNumber = 5;
    private float rotationY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float RotationY {
      get { return rotationY_; }
      set {
        rotationY_ = value;
      }
    }

    /// <summary>Field number for the "isFirstTouch" field.</summary>
    public const int IsFirstTouchFieldNumber = 6;
    private uint isFirstTouch_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint IsFirstTouch {
      get { return isFirstTouch_; }
      set {
        isFirstTouch_ = value;
      }
    }

    /// <summary>Field number for the "isSelecting" field.</summary>
    public const int IsSelectingFieldNumber = 7;
    private uint isSelecting_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint IsSelecting {
      get { return isSelecting_; }
      set {
        isSelecting_ = value;
      }
    }

    /// <summary>Field number for the "isFinished" field.</summary>
    public const int IsFinishedFieldNumber = 8;
    private uint isFinished_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint IsFinished {
      get { return isFinished_; }
      set {
        isFinished_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ObjectManipulation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ObjectManipulation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(TranslationX, other.TranslationX)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(TranslationY, other.TranslationY)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(TranslationZ, other.TranslationZ)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Scale, other.Scale)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(RotationY, other.RotationY)) return false;
      if (IsFirstTouch != other.IsFirstTouch) return false;
      if (IsSelecting != other.IsSelecting) return false;
      if (IsFinished != other.IsFinished) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (TranslationX != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(TranslationX);
      if (TranslationY != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(TranslationY);
      if (TranslationZ != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(TranslationZ);
      if (Scale != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Scale);
      if (RotationY != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(RotationY);
      if (IsFirstTouch != 0) hash ^= IsFirstTouch.GetHashCode();
      if (IsSelecting != 0) hash ^= IsSelecting.GetHashCode();
      if (IsFinished != 0) hash ^= IsFinished.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (TranslationX != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(TranslationX);
      }
      if (TranslationY != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(TranslationY);
      }
      if (TranslationZ != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(TranslationZ);
      }
      if (Scale != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Scale);
      }
      if (RotationY != 0F) {
        output.WriteRawTag(45);
        output.WriteFloat(RotationY);
      }
      if (IsFirstTouch != 0) {
        output.WriteRawTag(53);
        output.WriteFixed32(IsFirstTouch);
      }
      if (IsSelecting != 0) {
        output.WriteRawTag(61);
        output.WriteFixed32(IsSelecting);
      }
      if (IsFinished != 0) {
        output.WriteRawTag(69);
        output.WriteFixed32(IsFinished);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (TranslationX != 0F) {
        size += 1 + 4;
      }
      if (TranslationY != 0F) {
        size += 1 + 4;
      }
      if (TranslationZ != 0F) {
        size += 1 + 4;
      }
      if (Scale != 0F) {
        size += 1 + 4;
      }
      if (RotationY != 0F) {
        size += 1 + 4;
      }
      if (IsFirstTouch != 0) {
        size += 1 + 4;
      }
      if (IsSelecting != 0) {
        size += 1 + 4;
      }
      if (IsFinished != 0) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ObjectManipulation other) {
      if (other == null) {
        return;
      }
      if (other.TranslationX != 0F) {
        TranslationX = other.TranslationX;
      }
      if (other.TranslationY != 0F) {
        TranslationY = other.TranslationY;
      }
      if (other.TranslationZ != 0F) {
        TranslationZ = other.TranslationZ;
      }
      if (other.Scale != 0F) {
        Scale = other.Scale;
      }
      if (other.RotationY != 0F) {
        RotationY = other.RotationY;
      }
      if (other.IsFirstTouch != 0) {
        IsFirstTouch = other.IsFirstTouch;
      }
      if (other.IsSelecting != 0) {
        IsSelecting = other.IsSelecting;
      }
      if (other.IsFinished != 0) {
        IsFinished = other.IsFinished;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 13: {
            TranslationX = input.ReadFloat();
            break;
          }
          case 21: {
            TranslationY = input.ReadFloat();
            break;
          }
          case 29: {
            TranslationZ = input.ReadFloat();
            break;
          }
          case 37: {
            Scale = input.ReadFloat();
            break;
          }
          case 45: {
            RotationY = input.ReadFloat();
            break;
          }
          case 53: {
            IsFirstTouch = input.ReadFixed32();
            break;
          }
          case 61: {
            IsSelecting = input.ReadFixed32();
            break;
          }
          case 69: {
            IsFinished = input.ReadFixed32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class UIManipulation : pb::IMessage<UIManipulation> {
    private static readonly pb::MessageParser<UIManipulation> _parser = new pb::MessageParser<UIManipulation>(() => new UIManipulation());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<UIManipulation> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensAndroidMessagingReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIManipulation() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIManipulation(UIManipulation other) : this() {
      isSelecting_ = other.isSelecting_;
      scrollX_ = other.scrollX_;
      scrollY_ = other.scrollY_;
      hasAndroidUIInput_ = other.hasAndroidUIInput_;
      androidUIInput_ = other.androidUIInput_ != null ? other.androidUIInput_.Clone() : null;
      isFinished_ = other.isFinished_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public UIManipulation Clone() {
      return new UIManipulation(this);
    }

    /// <summary>Field number for the "isSelecting" field.</summary>
    public const int IsSelectingFieldNumber = 1;
    private uint isSelecting_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint IsSelecting {
      get { return isSelecting_; }
      set {
        isSelecting_ = value;
      }
    }

    /// <summary>Field number for the "scrollX" field.</summary>
    public const int ScrollXFieldNumber = 2;
    private float scrollX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float ScrollX {
      get { return scrollX_; }
      set {
        scrollX_ = value;
      }
    }

    /// <summary>Field number for the "scrollY" field.</summary>
    public const int ScrollYFieldNumber = 3;
    private float scrollY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public float ScrollY {
      get { return scrollY_; }
      set {
        scrollY_ = value;
      }
    }

    /// <summary>Field number for the "hasAndroidUIInput" field.</summary>
    public const int HasAndroidUIInputFieldNumber = 4;
    private uint hasAndroidUIInput_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint HasAndroidUIInput {
      get { return hasAndroidUIInput_; }
      set {
        hasAndroidUIInput_ = value;
      }
    }

    /// <summary>Field number for the "androidUIInput" field.</summary>
    public const int AndroidUIInputFieldNumber = 5;
    private global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput androidUIInput_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput AndroidUIInput {
      get { return androidUIInput_; }
      set {
        androidUIInput_ = value;
      }
    }

    /// <summary>Field number for the "isFinished" field.</summary>
    public const int IsFinishedFieldNumber = 6;
    private uint isFinished_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public uint IsFinished {
      get { return isFinished_; }
      set {
        isFinished_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as UIManipulation);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(UIManipulation other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsSelecting != other.IsSelecting) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(ScrollX, other.ScrollX)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(ScrollY, other.ScrollY)) return false;
      if (HasAndroidUIInput != other.HasAndroidUIInput) return false;
      if (!object.Equals(AndroidUIInput, other.AndroidUIInput)) return false;
      if (IsFinished != other.IsFinished) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (IsSelecting != 0) hash ^= IsSelecting.GetHashCode();
      if (ScrollX != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(ScrollX);
      if (ScrollY != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(ScrollY);
      if (HasAndroidUIInput != 0) hash ^= HasAndroidUIInput.GetHashCode();
      if (androidUIInput_ != null) hash ^= AndroidUIInput.GetHashCode();
      if (IsFinished != 0) hash ^= IsFinished.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (IsSelecting != 0) {
        output.WriteRawTag(13);
        output.WriteFixed32(IsSelecting);
      }
      if (ScrollX != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(ScrollX);
      }
      if (ScrollY != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(ScrollY);
      }
      if (HasAndroidUIInput != 0) {
        output.WriteRawTag(37);
        output.WriteFixed32(HasAndroidUIInput);
      }
      if (androidUIInput_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(AndroidUIInput);
      }
      if (IsFinished != 0) {
        output.WriteRawTag(53);
        output.WriteFixed32(IsFinished);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (IsSelecting != 0) {
        size += 1 + 4;
      }
      if (ScrollX != 0F) {
        size += 1 + 4;
      }
      if (ScrollY != 0F) {
        size += 1 + 4;
      }
      if (HasAndroidUIInput != 0) {
        size += 1 + 4;
      }
      if (androidUIInput_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AndroidUIInput);
      }
      if (IsFinished != 0) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(UIManipulation other) {
      if (other == null) {
        return;
      }
      if (other.IsSelecting != 0) {
        IsSelecting = other.IsSelecting;
      }
      if (other.ScrollX != 0F) {
        ScrollX = other.ScrollX;
      }
      if (other.ScrollY != 0F) {
        ScrollY = other.ScrollY;
      }
      if (other.HasAndroidUIInput != 0) {
        HasAndroidUIInput = other.HasAndroidUIInput;
      }
      if (other.androidUIInput_ != null) {
        if (androidUIInput_ == null) {
          AndroidUIInput = new global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput();
        }
        AndroidUIInput.MergeFrom(other.AndroidUIInput);
      }
      if (other.IsFinished != 0) {
        IsFinished = other.IsFinished;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 13: {
            IsSelecting = input.ReadFixed32();
            break;
          }
          case 21: {
            ScrollX = input.ReadFloat();
            break;
          }
          case 29: {
            ScrollY = input.ReadFloat();
            break;
          }
          case 37: {
            HasAndroidUIInput = input.ReadFixed32();
            break;
          }
          case 42: {
            if (androidUIInput_ == null) {
              AndroidUIInput = new global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Types.AndroidUIInput();
            }
            input.ReadMessage(AndroidUIInput);
            break;
          }
          case 53: {
            IsFinished = input.ReadFixed32();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the UIManipulation message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public sealed partial class AndroidUIInput : pb::IMessage<AndroidUIInput> {
        private static readonly pb::MessageParser<AndroidUIInput> _parser = new pb::MessageParser<AndroidUIInput>(() => new AndroidUIInput());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pb::MessageParser<AndroidUIInput> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::Google.Protobuf.HoloLensAndroidMessaging.UIManipulation.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AndroidUIInput() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AndroidUIInput(AndroidUIInput other) : this() {
          objectType_ = other.objectType_;
          quality_ = other.quality_;
          scale_ = other.scale_;
          color_ = other.color_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public AndroidUIInput Clone() {
          return new AndroidUIInput(this);
        }

        /// <summary>Field number for the "objectType" field.</summary>
        public const int ObjectTypeFieldNumber = 1;
        private uint objectType_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public uint ObjectType {
          get { return objectType_; }
          set {
            objectType_ = value;
          }
        }

        /// <summary>Field number for the "quality" field.</summary>
        public const int QualityFieldNumber = 2;
        private uint quality_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public uint Quality {
          get { return quality_; }
          set {
            quality_ = value;
          }
        }

        /// <summary>Field number for the "scale" field.</summary>
        public const int ScaleFieldNumber = 3;
        private uint scale_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public uint Scale {
          get { return scale_; }
          set {
            scale_ = value;
          }
        }

        /// <summary>Field number for the "color" field.</summary>
        public const int ColorFieldNumber = 4;
        private uint color_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public uint Color {
          get { return color_; }
          set {
            color_ = value;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override bool Equals(object other) {
          return Equals(other as AndroidUIInput);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public bool Equals(AndroidUIInput other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (ObjectType != other.ObjectType) return false;
          if (Quality != other.Quality) return false;
          if (Scale != other.Scale) return false;
          if (Color != other.Color) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override int GetHashCode() {
          int hash = 1;
          if (ObjectType != 0) hash ^= ObjectType.GetHashCode();
          if (Quality != 0) hash ^= Quality.GetHashCode();
          if (Scale != 0) hash ^= Scale.GetHashCode();
          if (Color != 0) hash ^= Color.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void WriteTo(pb::CodedOutputStream output) {
          if (ObjectType != 0) {
            output.WriteRawTag(13);
            output.WriteFixed32(ObjectType);
          }
          if (Quality != 0) {
            output.WriteRawTag(21);
            output.WriteFixed32(Quality);
          }
          if (Scale != 0) {
            output.WriteRawTag(29);
            output.WriteFixed32(Scale);
          }
          if (Color != 0) {
            output.WriteRawTag(37);
            output.WriteFixed32(Color);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public int CalculateSize() {
          int size = 0;
          if (ObjectType != 0) {
            size += 1 + 4;
          }
          if (Quality != 0) {
            size += 1 + 4;
          }
          if (Scale != 0) {
            size += 1 + 4;
          }
          if (Color != 0) {
            size += 1 + 4;
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(AndroidUIInput other) {
          if (other == null) {
            return;
          }
          if (other.ObjectType != 0) {
            ObjectType = other.ObjectType;
          }
          if (other.Quality != 0) {
            Quality = other.Quality;
          }
          if (other.Scale != 0) {
            Scale = other.Scale;
          }
          if (other.Color != 0) {
            Color = other.Color;
          }
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        public void MergeFrom(pb::CodedInputStream input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
            switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 13: {
                ObjectType = input.ReadFixed32();
                break;
              }
              case 21: {
                Quality = input.ReadFixed32();
                break;
              }
              case 29: {
                Scale = input.ReadFixed32();
                break;
              }
              case 37: {
                Color = input.ReadFixed32();
                break;
              }
            }
          }
        }

      }

    }
    #endregion

  }

  public sealed partial class HoloLensMessage : pb::IMessage<HoloLensMessage> {
    private static readonly pb::MessageParser<HoloLensMessage> _parser = new pb::MessageParser<HoloLensMessage>(() => new HoloLensMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<HoloLensMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensAndroidMessagingReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HoloLensMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HoloLensMessage(HoloLensMessage other) : this() {
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public HoloLensMessage Clone() {
      return new HoloLensMessage(this);
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 1;
    private global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage.Types.Message message_ = 0;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage.Types.Message Message {
      get { return message_; }
      set {
        message_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as HoloLensMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(HoloLensMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Message != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Message != 0) {
        output.WriteRawTag(8);
        output.WriteEnum((int) Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Message != 0) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(HoloLensMessage other) {
      if (other == null) {
        return;
      }
      if (other.Message != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Message = (global::Google.Protobuf.HoloLensAndroidMessaging.HoloLensMessage.Types.Message) input.ReadEnum();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the HoloLensMessage message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum Message {
        [pbr::OriginalName("USELESS_CONSTANT_NEVER_USE")] UselessConstantNeverUse = 0,
        [pbr::OriginalName("USE_CASE_OBJECT_TRANSLATION")] UseCaseObjectTranslation = 1,
        [pbr::OriginalName("USE_CASE_CLEAR")] UseCaseClear = 2,
        [pbr::OriginalName("USE_CASE_UI_MANIPULATION")] UseCaseUiManipulation = 3,
        [pbr::OriginalName("USE_CASE_UI_MANIPULATION_ANDROID_UI")] UseCaseUiManipulationAndroidUi = 4,
        [pbr::OriginalName("OBJECT_MANIPULATION_FINISHED")] ObjectManipulationFinished = 5,
        [pbr::OriginalName("OBJECT_FOR_MANIPULATION_SELECTED")] ObjectForManipulationSelected = 6,
        [pbr::OriginalName("UI_MANIPULATION_CORRECT")] UiManipulationCorrect = 7,
        [pbr::OriginalName("UI_MANIPULATION_FALSE")] UiManipulationFalse = 8,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
