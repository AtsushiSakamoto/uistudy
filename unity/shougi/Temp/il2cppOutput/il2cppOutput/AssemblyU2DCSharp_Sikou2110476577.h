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

// Sikou
struct  Sikou_t2110476577  : public Il2CppObject
{
public:
	// Joseki Sikou::joseki
	Joseki_t3207443615 * ___joseki_0;
	// System.Boolean Sikou::isJoseki
	bool ___isJoseki_1;
	// Te[0...,0...] Sikou::best
	TeU5BU2CU5D_t855127995* ___best_4;
	// System.Int32 Sikou::leaf
	int32_t ___leaf_5;
	// System.Int32 Sikou::node
	int32_t ___node_6;

public:
	inline static int32_t get_offset_of_joseki_0() { return static_cast<int32_t>(offsetof(Sikou_t2110476577, ___joseki_0)); }
	inline Joseki_t3207443615 * get_joseki_0() const { return ___joseki_0; }
	inline Joseki_t3207443615 ** get_address_of_joseki_0() { return &___joseki_0; }
	inline void set_joseki_0(Joseki_t3207443615 * value)
	{
		___joseki_0 = value;
		Il2CppCodeGenWriteBarrier(&___joseki_0, value);
	}

	inline static int32_t get_offset_of_isJoseki_1() { return static_cast<int32_t>(offsetof(Sikou_t2110476577, ___isJoseki_1)); }
	inline bool get_isJoseki_1() const { return ___isJoseki_1; }
	inline bool* get_address_of_isJoseki_1() { return &___isJoseki_1; }
	inline void set_isJoseki_1(bool value)
	{
		___isJoseki_1 = value;
	}

	inline static int32_t get_offset_of_best_4() { return static_cast<int32_t>(offsetof(Sikou_t2110476577, ___best_4)); }
	inline TeU5BU2CU5D_t855127995* get_best_4() const { return ___best_4; }
	inline TeU5BU2CU5D_t855127995** get_address_of_best_4() { return &___best_4; }
	inline void set_best_4(TeU5BU2CU5D_t855127995* value)
	{
		___best_4 = value;
		Il2CppCodeGenWriteBarrier(&___best_4, value);
	}

	inline static int32_t get_offset_of_leaf_5() { return static_cast<int32_t>(offsetof(Sikou_t2110476577, ___leaf_5)); }
	inline int32_t get_leaf_5() const { return ___leaf_5; }
	inline int32_t* get_address_of_leaf_5() { return &___leaf_5; }
	inline void set_leaf_5(int32_t value)
	{
		___leaf_5 = value;
	}

	inline static int32_t get_offset_of_node_6() { return static_cast<int32_t>(offsetof(Sikou_t2110476577, ___node_6)); }
	inline int32_t get_node_6() const { return ___node_6; }
	inline int32_t* get_address_of_node_6() { return &___node_6; }
	inline void set_node_6(int32_t value)
	{
		___node_6 = value;
	}
};

struct Sikou_t2110476577_StaticFields
{
public:
	// System.Int32 Sikou::DEPTH_MAX
	int32_t ___DEPTH_MAX_2;
	// System.Int32 Sikou::LIMIT_DEPTH
	int32_t ___LIMIT_DEPTH_3;

public:
	inline static int32_t get_offset_of_DEPTH_MAX_2() { return static_cast<int32_t>(offsetof(Sikou_t2110476577_StaticFields, ___DEPTH_MAX_2)); }
	inline int32_t get_DEPTH_MAX_2() const { return ___DEPTH_MAX_2; }
	inline int32_t* get_address_of_DEPTH_MAX_2() { return &___DEPTH_MAX_2; }
	inline void set_DEPTH_MAX_2(int32_t value)
	{
		___DEPTH_MAX_2 = value;
	}

	inline static int32_t get_offset_of_LIMIT_DEPTH_3() { return static_cast<int32_t>(offsetof(Sikou_t2110476577_StaticFields, ___LIMIT_DEPTH_3)); }
	inline int32_t get_LIMIT_DEPTH_3() const { return ___LIMIT_DEPTH_3; }
	inline int32_t* get_address_of_LIMIT_DEPTH_3() { return &___LIMIT_DEPTH_3; }
	inline void set_LIMIT_DEPTH_3(int32_t value)
	{
		___LIMIT_DEPTH_3 = value;
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
