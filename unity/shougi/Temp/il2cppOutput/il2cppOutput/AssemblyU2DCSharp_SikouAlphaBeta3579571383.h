#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

#include "mscorlib_System_Object2689449295.h"

// Joseki
struct Joseki_t3207443615;
// Te[0...,0...]
struct TeU5BU2CU5D_t855127995;




#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// SikouAlphaBeta
struct  SikouAlphaBeta_t3579571383  : public Il2CppObject
{
public:
	// Joseki SikouAlphaBeta::joseki
	Joseki_t3207443615 * ___joseki_0;
	// Te[0...,0...] SikouAlphaBeta::best
	TeU5BU2CU5D_t855127995* ___best_3;
	// System.Int32 SikouAlphaBeta::leaf
	int32_t ___leaf_4;
	// System.Int32 SikouAlphaBeta::node
	int32_t ___node_5;

public:
	inline static int32_t get_offset_of_joseki_0() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383, ___joseki_0)); }
	inline Joseki_t3207443615 * get_joseki_0() const { return ___joseki_0; }
	inline Joseki_t3207443615 ** get_address_of_joseki_0() { return &___joseki_0; }
	inline void set_joseki_0(Joseki_t3207443615 * value)
	{
		___joseki_0 = value;
		Il2CppCodeGenWriteBarrier(&___joseki_0, value);
	}

	inline static int32_t get_offset_of_best_3() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383, ___best_3)); }
	inline TeU5BU2CU5D_t855127995* get_best_3() const { return ___best_3; }
	inline TeU5BU2CU5D_t855127995** get_address_of_best_3() { return &___best_3; }
	inline void set_best_3(TeU5BU2CU5D_t855127995* value)
	{
		___best_3 = value;
		Il2CppCodeGenWriteBarrier(&___best_3, value);
	}

	inline static int32_t get_offset_of_leaf_4() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383, ___leaf_4)); }
	inline int32_t get_leaf_4() const { return ___leaf_4; }
	inline int32_t* get_address_of_leaf_4() { return &___leaf_4; }
	inline void set_leaf_4(int32_t value)
	{
		___leaf_4 = value;
	}

	inline static int32_t get_offset_of_node_5() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383, ___node_5)); }
	inline int32_t get_node_5() const { return ___node_5; }
	inline int32_t* get_address_of_node_5() { return &___node_5; }
	inline void set_node_5(int32_t value)
	{
		___node_5 = value;
	}
};

struct SikouAlphaBeta_t3579571383_StaticFields
{
public:
	// System.Int32 SikouAlphaBeta::DEPTH_MAX
	int32_t ___DEPTH_MAX_1;
	// System.Int32 SikouAlphaBeta::LIMIT_DEPTH
	int32_t ___LIMIT_DEPTH_2;

public:
	inline static int32_t get_offset_of_DEPTH_MAX_1() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383_StaticFields, ___DEPTH_MAX_1)); }
	inline int32_t get_DEPTH_MAX_1() const { return ___DEPTH_MAX_1; }
	inline int32_t* get_address_of_DEPTH_MAX_1() { return &___DEPTH_MAX_1; }
	inline void set_DEPTH_MAX_1(int32_t value)
	{
		___DEPTH_MAX_1 = value;
	}

	inline static int32_t get_offset_of_LIMIT_DEPTH_2() { return static_cast<int32_t>(offsetof(SikouAlphaBeta_t3579571383_StaticFields, ___LIMIT_DEPTH_2)); }
	inline int32_t get_LIMIT_DEPTH_2() const { return ___LIMIT_DEPTH_2; }
	inline int32_t* get_address_of_LIMIT_DEPTH_2() { return &___LIMIT_DEPTH_2; }
	inline void set_LIMIT_DEPTH_2(int32_t value)
	{
		___LIMIT_DEPTH_2 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
