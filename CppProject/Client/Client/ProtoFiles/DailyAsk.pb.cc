// Generated by the protocol buffer compiler.  DO NOT EDIT!
// NO CHECKED-IN PROTOBUF GENCODE
// source: DailyAsk.proto
// Protobuf C++ Version: 5.28.0-dev

#include "DailyAsk.pb.h"

#include <algorithm>
#include <type_traits>
#include "google/protobuf/io/coded_stream.h"
#include "google/protobuf/generated_message_tctable_impl.h"
#include "google/protobuf/extension_set.h"
#include "google/protobuf/wire_format_lite.h"
#include "google/protobuf/descriptor.h"
#include "google/protobuf/generated_message_reflection.h"
#include "google/protobuf/reflection_ops.h"
#include "google/protobuf/wire_format.h"
// @@protoc_insertion_point(includes)

// Must be included last.
#include "google/protobuf/port_def.inc"
PROTOBUF_PRAGMA_INIT_SEG
namespace _pb = ::google::protobuf;
namespace _pbi = ::google::protobuf::internal;
namespace _fl = ::google::protobuf::internal::field_layout;

inline constexpr S2CDailyAsk::Impl_::Impl_(
    ::_pbi::ConstantInitialized) noexcept
      : content_(
            &::google::protobuf::internal::fixed_address_empty_string,
            ::_pbi::ConstantInitialized()),
        _cached_size_{0} {}

template <typename>
PROTOBUF_CONSTEXPR S2CDailyAsk::S2CDailyAsk(::_pbi::ConstantInitialized)
    : _impl_(::_pbi::ConstantInitialized()) {}
struct S2CDailyAskDefaultTypeInternal {
  PROTOBUF_CONSTEXPR S2CDailyAskDefaultTypeInternal() : _instance(::_pbi::ConstantInitialized{}) {}
  ~S2CDailyAskDefaultTypeInternal() {}
  union {
    S2CDailyAsk _instance;
  };
};

PROTOBUF_ATTRIBUTE_NO_DESTROY PROTOBUF_CONSTINIT
    PROTOBUF_ATTRIBUTE_INIT_PRIORITY1 S2CDailyAskDefaultTypeInternal _S2CDailyAsk_default_instance_;

inline constexpr C2SDailyAsk::Impl_::Impl_(
    ::_pbi::ConstantInitialized) noexcept
      : content_(
            &::google::protobuf::internal::fixed_address_empty_string,
            ::_pbi::ConstantInitialized()),
        _cached_size_{0} {}

template <typename>
PROTOBUF_CONSTEXPR C2SDailyAsk::C2SDailyAsk(::_pbi::ConstantInitialized)
    : _impl_(::_pbi::ConstantInitialized()) {}
struct C2SDailyAskDefaultTypeInternal {
  PROTOBUF_CONSTEXPR C2SDailyAskDefaultTypeInternal() : _instance(::_pbi::ConstantInitialized{}) {}
  ~C2SDailyAskDefaultTypeInternal() {}
  union {
    C2SDailyAsk _instance;
  };
};

PROTOBUF_ATTRIBUTE_NO_DESTROY PROTOBUF_CONSTINIT
    PROTOBUF_ATTRIBUTE_INIT_PRIORITY1 C2SDailyAskDefaultTypeInternal _C2SDailyAsk_default_instance_;
static constexpr const ::_pb::EnumDescriptor**
    file_level_enum_descriptors_DailyAsk_2eproto = nullptr;
static constexpr const ::_pb::ServiceDescriptor**
    file_level_service_descriptors_DailyAsk_2eproto = nullptr;
const ::uint32_t
    TableStruct_DailyAsk_2eproto::offsets[] ABSL_ATTRIBUTE_SECTION_VARIABLE(
        protodesc_cold) = {
        ~0u,  // no _has_bits_
        PROTOBUF_FIELD_OFFSET(::C2SDailyAsk, _internal_metadata_),
        ~0u,  // no _extensions_
        ~0u,  // no _oneof_case_
        ~0u,  // no _weak_field_map_
        ~0u,  // no _inlined_string_donated_
        ~0u,  // no _split_
        ~0u,  // no sizeof(Split)
        PROTOBUF_FIELD_OFFSET(::C2SDailyAsk, _impl_.content_),
        ~0u,  // no _has_bits_
        PROTOBUF_FIELD_OFFSET(::S2CDailyAsk, _internal_metadata_),
        ~0u,  // no _extensions_
        ~0u,  // no _oneof_case_
        ~0u,  // no _weak_field_map_
        ~0u,  // no _inlined_string_donated_
        ~0u,  // no _split_
        ~0u,  // no sizeof(Split)
        PROTOBUF_FIELD_OFFSET(::S2CDailyAsk, _impl_.content_),
};

static const ::_pbi::MigrationSchema
    schemas[] ABSL_ATTRIBUTE_SECTION_VARIABLE(protodesc_cold) = {
        {0, -1, -1, sizeof(::C2SDailyAsk)},
        {9, -1, -1, sizeof(::S2CDailyAsk)},
};
static const ::_pb::Message* const file_default_instances[] = {
    &::_C2SDailyAsk_default_instance_._instance,
    &::_S2CDailyAsk_default_instance_._instance,
};
const char descriptor_table_protodef_DailyAsk_2eproto[] ABSL_ATTRIBUTE_SECTION_VARIABLE(
    protodesc_cold) = {
    "\n\016DailyAsk.proto\"\036\n\013C2SDailyAsk\022\017\n\007Conte"
    "nt\030\001 \001(\t\"\036\n\013S2CDailyAsk\022\017\n\007Content\030\002 \001(\t"
    "b\006proto3"
};
static ::absl::once_flag descriptor_table_DailyAsk_2eproto_once;
PROTOBUF_CONSTINIT const ::_pbi::DescriptorTable descriptor_table_DailyAsk_2eproto = {
    false,
    false,
    88,
    descriptor_table_protodef_DailyAsk_2eproto,
    "DailyAsk.proto",
    &descriptor_table_DailyAsk_2eproto_once,
    nullptr,
    0,
    2,
    schemas,
    file_default_instances,
    TableStruct_DailyAsk_2eproto::offsets,
    file_level_enum_descriptors_DailyAsk_2eproto,
    file_level_service_descriptors_DailyAsk_2eproto,
};
// ===================================================================

class C2SDailyAsk::_Internal {
 public:
};

C2SDailyAsk::C2SDailyAsk(::google::protobuf::Arena* arena)
    : ::google::protobuf::Message(arena) {
  SharedCtor(arena);
  // @@protoc_insertion_point(arena_constructor:C2SDailyAsk)
}
inline PROTOBUF_NDEBUG_INLINE C2SDailyAsk::Impl_::Impl_(
    ::google::protobuf::internal::InternalVisibility visibility, ::google::protobuf::Arena* arena,
    const Impl_& from, const ::C2SDailyAsk& from_msg)
      : content_(arena, from.content_),
        _cached_size_{0} {}

C2SDailyAsk::C2SDailyAsk(
    ::google::protobuf::Arena* arena,
    const C2SDailyAsk& from)
    : ::google::protobuf::Message(arena) {
  C2SDailyAsk* const _this = this;
  (void)_this;
  _internal_metadata_.MergeFrom<::google::protobuf::UnknownFieldSet>(
      from._internal_metadata_);
  new (&_impl_) Impl_(internal_visibility(), arena, from._impl_, from);

  // @@protoc_insertion_point(copy_constructor:C2SDailyAsk)
}
inline PROTOBUF_NDEBUG_INLINE C2SDailyAsk::Impl_::Impl_(
    ::google::protobuf::internal::InternalVisibility visibility,
    ::google::protobuf::Arena* arena)
      : content_(arena),
        _cached_size_{0} {}

inline void C2SDailyAsk::SharedCtor(::_pb::Arena* arena) {
  new (&_impl_) Impl_(internal_visibility(), arena);
}
C2SDailyAsk::~C2SDailyAsk() {
  // @@protoc_insertion_point(destructor:C2SDailyAsk)
  _internal_metadata_.Delete<::google::protobuf::UnknownFieldSet>();
  SharedDtor();
}
inline void C2SDailyAsk::SharedDtor() {
  ABSL_DCHECK(GetArena() == nullptr);
  _impl_.content_.Destroy();
  _impl_.~Impl_();
}

const ::google::protobuf::MessageLite::ClassData*
C2SDailyAsk::GetClassData() const {
  PROTOBUF_CONSTINIT static const ::google::protobuf::MessageLite::
      ClassDataFull _data_ = {
          {
              &_table_.header,
              nullptr,  // OnDemandRegisterArenaDtor
              nullptr,  // IsInitialized
              PROTOBUF_FIELD_OFFSET(C2SDailyAsk, _impl_._cached_size_),
              false,
          },
          &C2SDailyAsk::MergeImpl,
          &C2SDailyAsk::kDescriptorMethods,
          &descriptor_table_DailyAsk_2eproto,
          nullptr,  // tracker
      };
  ::google::protobuf::internal::PrefetchToLocalCache(&_data_);
  ::google::protobuf::internal::PrefetchToLocalCache(_data_.tc_table);
  return _data_.base();
}
PROTOBUF_CONSTINIT PROTOBUF_ATTRIBUTE_INIT_PRIORITY1
const ::_pbi::TcParseTable<0, 1, 0, 27, 2> C2SDailyAsk::_table_ = {
  {
    0,  // no _has_bits_
    0, // no _extensions_
    1, 0,  // max_field_number, fast_idx_mask
    offsetof(decltype(_table_), field_lookup_table),
    4294967294,  // skipmap
    offsetof(decltype(_table_), field_entries),
    1,  // num_field_entries
    0,  // num_aux_entries
    offsetof(decltype(_table_), field_names),  // no aux_entries
    &_C2SDailyAsk_default_instance_._instance,
    nullptr,  // post_loop_handler
    ::_pbi::TcParser::GenericFallback,  // fallback
    #ifdef PROTOBUF_PREFETCH_PARSE_TABLE
    ::_pbi::TcParser::GetTable<::C2SDailyAsk>(),  // to_prefetch
    #endif  // PROTOBUF_PREFETCH_PARSE_TABLE
  }, {{
    // string Content = 1;
    {::_pbi::TcParser::FastUS1,
     {10, 63, 0, PROTOBUF_FIELD_OFFSET(C2SDailyAsk, _impl_.content_)}},
  }}, {{
    65535, 65535
  }}, {{
    // string Content = 1;
    {PROTOBUF_FIELD_OFFSET(C2SDailyAsk, _impl_.content_), 0, 0,
    (0 | ::_fl::kFcSingular | ::_fl::kUtf8String | ::_fl::kRepAString)},
  }},
  // no aux_entries
  {{
    "\13\7\0\0\0\0\0\0"
    "C2SDailyAsk"
    "Content"
  }},
};

PROTOBUF_NOINLINE void C2SDailyAsk::Clear() {
// @@protoc_insertion_point(message_clear_start:C2SDailyAsk)
  ::google::protobuf::internal::TSanWrite(&_impl_);
  ::uint32_t cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  _impl_.content_.ClearToEmpty();
  _internal_metadata_.Clear<::google::protobuf::UnknownFieldSet>();
}

::uint8_t* C2SDailyAsk::_InternalSerialize(
    ::uint8_t* target,
    ::google::protobuf::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:C2SDailyAsk)
  ::uint32_t cached_has_bits = 0;
  (void)cached_has_bits;

  // string Content = 1;
  if (!this->_internal_content().empty()) {
    const std::string& _s = this->_internal_content();
    ::google::protobuf::internal::WireFormatLite::VerifyUtf8String(
        _s.data(), static_cast<int>(_s.length()), ::google::protobuf::internal::WireFormatLite::SERIALIZE, "C2SDailyAsk.Content");
    target = stream->WriteStringMaybeAliased(1, _s, target);
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target =
        ::_pbi::WireFormat::InternalSerializeUnknownFieldsToArray(
            _internal_metadata_.unknown_fields<::google::protobuf::UnknownFieldSet>(::google::protobuf::UnknownFieldSet::default_instance), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:C2SDailyAsk)
  return target;
}

::size_t C2SDailyAsk::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:C2SDailyAsk)
  ::size_t total_size = 0;

  ::uint32_t cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // string Content = 1;
  if (!this->_internal_content().empty()) {
    total_size += 1 + ::google::protobuf::internal::WireFormatLite::StringSize(
                                    this->_internal_content());
  }

  return MaybeComputeUnknownFieldsSize(total_size, &_impl_._cached_size_);
}


void C2SDailyAsk::MergeImpl(::google::protobuf::MessageLite& to_msg, const ::google::protobuf::MessageLite& from_msg) {
  auto* const _this = static_cast<C2SDailyAsk*>(&to_msg);
  auto& from = static_cast<const C2SDailyAsk&>(from_msg);
  // @@protoc_insertion_point(class_specific_merge_from_start:C2SDailyAsk)
  ABSL_DCHECK_NE(&from, _this);
  ::uint32_t cached_has_bits = 0;
  (void) cached_has_bits;

  if (!from._internal_content().empty()) {
    _this->_internal_set_content(from._internal_content());
  }
  _this->_internal_metadata_.MergeFrom<::google::protobuf::UnknownFieldSet>(from._internal_metadata_);
}

void C2SDailyAsk::CopyFrom(const C2SDailyAsk& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:C2SDailyAsk)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}


void C2SDailyAsk::InternalSwap(C2SDailyAsk* PROTOBUF_RESTRICT other) {
  using std::swap;
  auto* arena = GetArena();
  ABSL_DCHECK_EQ(arena, other->GetArena());
  _internal_metadata_.InternalSwap(&other->_internal_metadata_);
  ::_pbi::ArenaStringPtr::InternalSwap(&_impl_.content_, &other->_impl_.content_, arena);
}

::google::protobuf::Metadata C2SDailyAsk::GetMetadata() const {
  return ::google::protobuf::Message::GetMetadataImpl(GetClassData()->full());
}
// ===================================================================

class S2CDailyAsk::_Internal {
 public:
};

S2CDailyAsk::S2CDailyAsk(::google::protobuf::Arena* arena)
    : ::google::protobuf::Message(arena) {
  SharedCtor(arena);
  // @@protoc_insertion_point(arena_constructor:S2CDailyAsk)
}
inline PROTOBUF_NDEBUG_INLINE S2CDailyAsk::Impl_::Impl_(
    ::google::protobuf::internal::InternalVisibility visibility, ::google::protobuf::Arena* arena,
    const Impl_& from, const ::S2CDailyAsk& from_msg)
      : content_(arena, from.content_),
        _cached_size_{0} {}

S2CDailyAsk::S2CDailyAsk(
    ::google::protobuf::Arena* arena,
    const S2CDailyAsk& from)
    : ::google::protobuf::Message(arena) {
  S2CDailyAsk* const _this = this;
  (void)_this;
  _internal_metadata_.MergeFrom<::google::protobuf::UnknownFieldSet>(
      from._internal_metadata_);
  new (&_impl_) Impl_(internal_visibility(), arena, from._impl_, from);

  // @@protoc_insertion_point(copy_constructor:S2CDailyAsk)
}
inline PROTOBUF_NDEBUG_INLINE S2CDailyAsk::Impl_::Impl_(
    ::google::protobuf::internal::InternalVisibility visibility,
    ::google::protobuf::Arena* arena)
      : content_(arena),
        _cached_size_{0} {}

inline void S2CDailyAsk::SharedCtor(::_pb::Arena* arena) {
  new (&_impl_) Impl_(internal_visibility(), arena);
}
S2CDailyAsk::~S2CDailyAsk() {
  // @@protoc_insertion_point(destructor:S2CDailyAsk)
  _internal_metadata_.Delete<::google::protobuf::UnknownFieldSet>();
  SharedDtor();
}
inline void S2CDailyAsk::SharedDtor() {
  ABSL_DCHECK(GetArena() == nullptr);
  _impl_.content_.Destroy();
  _impl_.~Impl_();
}

const ::google::protobuf::MessageLite::ClassData*
S2CDailyAsk::GetClassData() const {
  PROTOBUF_CONSTINIT static const ::google::protobuf::MessageLite::
      ClassDataFull _data_ = {
          {
              &_table_.header,
              nullptr,  // OnDemandRegisterArenaDtor
              nullptr,  // IsInitialized
              PROTOBUF_FIELD_OFFSET(S2CDailyAsk, _impl_._cached_size_),
              false,
          },
          &S2CDailyAsk::MergeImpl,
          &S2CDailyAsk::kDescriptorMethods,
          &descriptor_table_DailyAsk_2eproto,
          nullptr,  // tracker
      };
  ::google::protobuf::internal::PrefetchToLocalCache(&_data_);
  ::google::protobuf::internal::PrefetchToLocalCache(_data_.tc_table);
  return _data_.base();
}
PROTOBUF_CONSTINIT PROTOBUF_ATTRIBUTE_INIT_PRIORITY1
const ::_pbi::TcParseTable<0, 1, 0, 27, 2> S2CDailyAsk::_table_ = {
  {
    0,  // no _has_bits_
    0, // no _extensions_
    2, 0,  // max_field_number, fast_idx_mask
    offsetof(decltype(_table_), field_lookup_table),
    4294967293,  // skipmap
    offsetof(decltype(_table_), field_entries),
    1,  // num_field_entries
    0,  // num_aux_entries
    offsetof(decltype(_table_), field_names),  // no aux_entries
    &_S2CDailyAsk_default_instance_._instance,
    nullptr,  // post_loop_handler
    ::_pbi::TcParser::GenericFallback,  // fallback
    #ifdef PROTOBUF_PREFETCH_PARSE_TABLE
    ::_pbi::TcParser::GetTable<::S2CDailyAsk>(),  // to_prefetch
    #endif  // PROTOBUF_PREFETCH_PARSE_TABLE
  }, {{
    // string Content = 2;
    {::_pbi::TcParser::FastUS1,
     {18, 63, 0, PROTOBUF_FIELD_OFFSET(S2CDailyAsk, _impl_.content_)}},
  }}, {{
    65535, 65535
  }}, {{
    // string Content = 2;
    {PROTOBUF_FIELD_OFFSET(S2CDailyAsk, _impl_.content_), 0, 0,
    (0 | ::_fl::kFcSingular | ::_fl::kUtf8String | ::_fl::kRepAString)},
  }},
  // no aux_entries
  {{
    "\13\7\0\0\0\0\0\0"
    "S2CDailyAsk"
    "Content"
  }},
};

PROTOBUF_NOINLINE void S2CDailyAsk::Clear() {
// @@protoc_insertion_point(message_clear_start:S2CDailyAsk)
  ::google::protobuf::internal::TSanWrite(&_impl_);
  ::uint32_t cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  _impl_.content_.ClearToEmpty();
  _internal_metadata_.Clear<::google::protobuf::UnknownFieldSet>();
}

::uint8_t* S2CDailyAsk::_InternalSerialize(
    ::uint8_t* target,
    ::google::protobuf::io::EpsCopyOutputStream* stream) const {
  // @@protoc_insertion_point(serialize_to_array_start:S2CDailyAsk)
  ::uint32_t cached_has_bits = 0;
  (void)cached_has_bits;

  // string Content = 2;
  if (!this->_internal_content().empty()) {
    const std::string& _s = this->_internal_content();
    ::google::protobuf::internal::WireFormatLite::VerifyUtf8String(
        _s.data(), static_cast<int>(_s.length()), ::google::protobuf::internal::WireFormatLite::SERIALIZE, "S2CDailyAsk.Content");
    target = stream->WriteStringMaybeAliased(2, _s, target);
  }

  if (PROTOBUF_PREDICT_FALSE(_internal_metadata_.have_unknown_fields())) {
    target =
        ::_pbi::WireFormat::InternalSerializeUnknownFieldsToArray(
            _internal_metadata_.unknown_fields<::google::protobuf::UnknownFieldSet>(::google::protobuf::UnknownFieldSet::default_instance), target, stream);
  }
  // @@protoc_insertion_point(serialize_to_array_end:S2CDailyAsk)
  return target;
}

::size_t S2CDailyAsk::ByteSizeLong() const {
// @@protoc_insertion_point(message_byte_size_start:S2CDailyAsk)
  ::size_t total_size = 0;

  ::uint32_t cached_has_bits = 0;
  // Prevent compiler warnings about cached_has_bits being unused
  (void) cached_has_bits;

  // string Content = 2;
  if (!this->_internal_content().empty()) {
    total_size += 1 + ::google::protobuf::internal::WireFormatLite::StringSize(
                                    this->_internal_content());
  }

  return MaybeComputeUnknownFieldsSize(total_size, &_impl_._cached_size_);
}


void S2CDailyAsk::MergeImpl(::google::protobuf::MessageLite& to_msg, const ::google::protobuf::MessageLite& from_msg) {
  auto* const _this = static_cast<S2CDailyAsk*>(&to_msg);
  auto& from = static_cast<const S2CDailyAsk&>(from_msg);
  // @@protoc_insertion_point(class_specific_merge_from_start:S2CDailyAsk)
  ABSL_DCHECK_NE(&from, _this);
  ::uint32_t cached_has_bits = 0;
  (void) cached_has_bits;

  if (!from._internal_content().empty()) {
    _this->_internal_set_content(from._internal_content());
  }
  _this->_internal_metadata_.MergeFrom<::google::protobuf::UnknownFieldSet>(from._internal_metadata_);
}

void S2CDailyAsk::CopyFrom(const S2CDailyAsk& from) {
// @@protoc_insertion_point(class_specific_copy_from_start:S2CDailyAsk)
  if (&from == this) return;
  Clear();
  MergeFrom(from);
}


void S2CDailyAsk::InternalSwap(S2CDailyAsk* PROTOBUF_RESTRICT other) {
  using std::swap;
  auto* arena = GetArena();
  ABSL_DCHECK_EQ(arena, other->GetArena());
  _internal_metadata_.InternalSwap(&other->_internal_metadata_);
  ::_pbi::ArenaStringPtr::InternalSwap(&_impl_.content_, &other->_impl_.content_, arena);
}

::google::protobuf::Metadata S2CDailyAsk::GetMetadata() const {
  return ::google::protobuf::Message::GetMetadataImpl(GetClassData()->full());
}
// @@protoc_insertion_point(namespace_scope)
namespace google {
namespace protobuf {
}  // namespace protobuf
}  // namespace google
// @@protoc_insertion_point(global_scope)
PROTOBUF_ATTRIBUTE_INIT_PRIORITY2 static ::std::false_type
    _static_init2_ PROTOBUF_UNUSED =
        (::_pbi::AddDescriptors(&descriptor_table_DailyAsk_2eproto),
         ::std::false_type{});
#include "google/protobuf/port_undef.inc"
