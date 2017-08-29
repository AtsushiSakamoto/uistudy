#pragma once

#include "il2cpp-config.h"

#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif

#include <stdint.h>

#include "UnityEngine_UnityEngine_MonoBehaviour1158329972.h"

// UnityEngine.GameObject[]
struct GameObjectU5BU5D_t3057952154;
// UnityEngine.Sprite[]
struct SpriteU5BU5D_t3359083662;
// UnityEngine.GameObject
struct GameObject_t1756533147;
// System.String
struct String_t;
// Kyokumenn
struct Kyokumenn_t1278207725;
// System.Collections.Generic.List`1<Te>
struct List_1_t1656987495;
// System.Collections.Generic.List`1<Kyokumenn>
struct List_1_t647328857;
// Te
struct Te_t2287866363;
// Sikou
struct Sikou_t2110476577;




#ifdef __clang__
#pragma clang diagnostic push
#pragma clang diagnostic ignored "-Winvalid-offsetof"
#pragma clang diagnostic ignored "-Wunused-variable"
#endif

// GameManager
struct  GameManager_t2252321495  : public MonoBehaviour_t1158329972
{
public:
	// UnityEngine.GameObject[] GameManager::Masu
	GameObjectU5BU5D_t3057952154* ___Masu_2;
	// UnityEngine.Sprite[] GameManager::komaPicture
	SpriteU5BU5D_t3359083662* ___komaPicture_3;
	// UnityEngine.GameObject[] GameManager::hand
	GameObjectU5BU5D_t3057952154* ___hand_4;
	// UnityEngine.GameObject[] GameManager::motiGoma
	GameObjectU5BU5D_t3057952154* ___motiGoma_5;
	// UnityEngine.GameObject GameManager::popupCanvas
	GameObject_t1756533147 * ___popupCanvas_6;
	// UnityEngine.GameObject GameManager::resultCanvas
	GameObject_t1756533147 * ___resultCanvas_7;
	// UnityEngine.GameObject GameManager::winner
	GameObject_t1756533147 * ___winner_8;
	// UnityEngine.GameObject GameManager::sente
	GameObject_t1756533147 * ___sente_9;
	// UnityEngine.GameObject GameManager::gote
	GameObject_t1756533147 * ___gote_10;
	// UnityEngine.GameObject GameManager::turn
	GameObject_t1756533147 * ___turn_11;
	// Kyokumenn GameManager::kk
	Kyokumenn_t1278207725 * ___kk_13;
	// System.Collections.Generic.List`1<Te> GameManager::kihu
	List_1_t1656987495 * ___kihu_14;
	// System.Collections.Generic.List`1<Kyokumenn> GameManager::historykyokumenn
	List_1_t647328857 * ___historykyokumenn_15;
	// Te GameManager::te
	Te_t2287866363 * ___te_16;
	// Sikou GameManager::sikou
	Sikou_t2110476577 * ___sikou_17;
	// System.Int32 GameManager::isSelectKoma
	int32_t ___isSelectKoma_18;
	// System.Int32 GameManager::isSelectMotigoma
	int32_t ___isSelectMotigoma_19;
	// System.Boolean GameManager::promote
	bool ___promote_20;
	// System.Boolean GameManager::vsCom
	bool ___vsCom_21;
	// System.Boolean GameManager::vsComGote
	bool ___vsComGote_22;
	// System.Boolean GameManager::pushButtonBool
	bool ___pushButtonBool_23;

public:
	inline static int32_t get_offset_of_Masu_2() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___Masu_2)); }
	inline GameObjectU5BU5D_t3057952154* get_Masu_2() const { return ___Masu_2; }
	inline GameObjectU5BU5D_t3057952154** get_address_of_Masu_2() { return &___Masu_2; }
	inline void set_Masu_2(GameObjectU5BU5D_t3057952154* value)
	{
		___Masu_2 = value;
		Il2CppCodeGenWriteBarrier(&___Masu_2, value);
	}

	inline static int32_t get_offset_of_komaPicture_3() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___komaPicture_3)); }
	inline SpriteU5BU5D_t3359083662* get_komaPicture_3() const { return ___komaPicture_3; }
	inline SpriteU5BU5D_t3359083662** get_address_of_komaPicture_3() { return &___komaPicture_3; }
	inline void set_komaPicture_3(SpriteU5BU5D_t3359083662* value)
	{
		___komaPicture_3 = value;
		Il2CppCodeGenWriteBarrier(&___komaPicture_3, value);
	}

	inline static int32_t get_offset_of_hand_4() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___hand_4)); }
	inline GameObjectU5BU5D_t3057952154* get_hand_4() const { return ___hand_4; }
	inline GameObjectU5BU5D_t3057952154** get_address_of_hand_4() { return &___hand_4; }
	inline void set_hand_4(GameObjectU5BU5D_t3057952154* value)
	{
		___hand_4 = value;
		Il2CppCodeGenWriteBarrier(&___hand_4, value);
	}

	inline static int32_t get_offset_of_motiGoma_5() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___motiGoma_5)); }
	inline GameObjectU5BU5D_t3057952154* get_motiGoma_5() const { return ___motiGoma_5; }
	inline GameObjectU5BU5D_t3057952154** get_address_of_motiGoma_5() { return &___motiGoma_5; }
	inline void set_motiGoma_5(GameObjectU5BU5D_t3057952154* value)
	{
		___motiGoma_5 = value;
		Il2CppCodeGenWriteBarrier(&___motiGoma_5, value);
	}

	inline static int32_t get_offset_of_popupCanvas_6() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___popupCanvas_6)); }
	inline GameObject_t1756533147 * get_popupCanvas_6() const { return ___popupCanvas_6; }
	inline GameObject_t1756533147 ** get_address_of_popupCanvas_6() { return &___popupCanvas_6; }
	inline void set_popupCanvas_6(GameObject_t1756533147 * value)
	{
		___popupCanvas_6 = value;
		Il2CppCodeGenWriteBarrier(&___popupCanvas_6, value);
	}

	inline static int32_t get_offset_of_resultCanvas_7() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___resultCanvas_7)); }
	inline GameObject_t1756533147 * get_resultCanvas_7() const { return ___resultCanvas_7; }
	inline GameObject_t1756533147 ** get_address_of_resultCanvas_7() { return &___resultCanvas_7; }
	inline void set_resultCanvas_7(GameObject_t1756533147 * value)
	{
		___resultCanvas_7 = value;
		Il2CppCodeGenWriteBarrier(&___resultCanvas_7, value);
	}

	inline static int32_t get_offset_of_winner_8() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___winner_8)); }
	inline GameObject_t1756533147 * get_winner_8() const { return ___winner_8; }
	inline GameObject_t1756533147 ** get_address_of_winner_8() { return &___winner_8; }
	inline void set_winner_8(GameObject_t1756533147 * value)
	{
		___winner_8 = value;
		Il2CppCodeGenWriteBarrier(&___winner_8, value);
	}

	inline static int32_t get_offset_of_sente_9() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___sente_9)); }
	inline GameObject_t1756533147 * get_sente_9() const { return ___sente_9; }
	inline GameObject_t1756533147 ** get_address_of_sente_9() { return &___sente_9; }
	inline void set_sente_9(GameObject_t1756533147 * value)
	{
		___sente_9 = value;
		Il2CppCodeGenWriteBarrier(&___sente_9, value);
	}

	inline static int32_t get_offset_of_gote_10() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___gote_10)); }
	inline GameObject_t1756533147 * get_gote_10() const { return ___gote_10; }
	inline GameObject_t1756533147 ** get_address_of_gote_10() { return &___gote_10; }
	inline void set_gote_10(GameObject_t1756533147 * value)
	{
		___gote_10 = value;
		Il2CppCodeGenWriteBarrier(&___gote_10, value);
	}

	inline static int32_t get_offset_of_turn_11() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___turn_11)); }
	inline GameObject_t1756533147 * get_turn_11() const { return ___turn_11; }
	inline GameObject_t1756533147 ** get_address_of_turn_11() { return &___turn_11; }
	inline void set_turn_11(GameObject_t1756533147 * value)
	{
		___turn_11 = value;
		Il2CppCodeGenWriteBarrier(&___turn_11, value);
	}

	inline static int32_t get_offset_of_kk_13() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___kk_13)); }
	inline Kyokumenn_t1278207725 * get_kk_13() const { return ___kk_13; }
	inline Kyokumenn_t1278207725 ** get_address_of_kk_13() { return &___kk_13; }
	inline void set_kk_13(Kyokumenn_t1278207725 * value)
	{
		___kk_13 = value;
		Il2CppCodeGenWriteBarrier(&___kk_13, value);
	}

	inline static int32_t get_offset_of_kihu_14() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___kihu_14)); }
	inline List_1_t1656987495 * get_kihu_14() const { return ___kihu_14; }
	inline List_1_t1656987495 ** get_address_of_kihu_14() { return &___kihu_14; }
	inline void set_kihu_14(List_1_t1656987495 * value)
	{
		___kihu_14 = value;
		Il2CppCodeGenWriteBarrier(&___kihu_14, value);
	}

	inline static int32_t get_offset_of_historykyokumenn_15() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___historykyokumenn_15)); }
	inline List_1_t647328857 * get_historykyokumenn_15() const { return ___historykyokumenn_15; }
	inline List_1_t647328857 ** get_address_of_historykyokumenn_15() { return &___historykyokumenn_15; }
	inline void set_historykyokumenn_15(List_1_t647328857 * value)
	{
		___historykyokumenn_15 = value;
		Il2CppCodeGenWriteBarrier(&___historykyokumenn_15, value);
	}

	inline static int32_t get_offset_of_te_16() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___te_16)); }
	inline Te_t2287866363 * get_te_16() const { return ___te_16; }
	inline Te_t2287866363 ** get_address_of_te_16() { return &___te_16; }
	inline void set_te_16(Te_t2287866363 * value)
	{
		___te_16 = value;
		Il2CppCodeGenWriteBarrier(&___te_16, value);
	}

	inline static int32_t get_offset_of_sikou_17() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___sikou_17)); }
	inline Sikou_t2110476577 * get_sikou_17() const { return ___sikou_17; }
	inline Sikou_t2110476577 ** get_address_of_sikou_17() { return &___sikou_17; }
	inline void set_sikou_17(Sikou_t2110476577 * value)
	{
		___sikou_17 = value;
		Il2CppCodeGenWriteBarrier(&___sikou_17, value);
	}

	inline static int32_t get_offset_of_isSelectKoma_18() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___isSelectKoma_18)); }
	inline int32_t get_isSelectKoma_18() const { return ___isSelectKoma_18; }
	inline int32_t* get_address_of_isSelectKoma_18() { return &___isSelectKoma_18; }
	inline void set_isSelectKoma_18(int32_t value)
	{
		___isSelectKoma_18 = value;
	}

	inline static int32_t get_offset_of_isSelectMotigoma_19() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___isSelectMotigoma_19)); }
	inline int32_t get_isSelectMotigoma_19() const { return ___isSelectMotigoma_19; }
	inline int32_t* get_address_of_isSelectMotigoma_19() { return &___isSelectMotigoma_19; }
	inline void set_isSelectMotigoma_19(int32_t value)
	{
		___isSelectMotigoma_19 = value;
	}

	inline static int32_t get_offset_of_promote_20() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___promote_20)); }
	inline bool get_promote_20() const { return ___promote_20; }
	inline bool* get_address_of_promote_20() { return &___promote_20; }
	inline void set_promote_20(bool value)
	{
		___promote_20 = value;
	}

	inline static int32_t get_offset_of_vsCom_21() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___vsCom_21)); }
	inline bool get_vsCom_21() const { return ___vsCom_21; }
	inline bool* get_address_of_vsCom_21() { return &___vsCom_21; }
	inline void set_vsCom_21(bool value)
	{
		___vsCom_21 = value;
	}

	inline static int32_t get_offset_of_vsComGote_22() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___vsComGote_22)); }
	inline bool get_vsComGote_22() const { return ___vsComGote_22; }
	inline bool* get_address_of_vsComGote_22() { return &___vsComGote_22; }
	inline void set_vsComGote_22(bool value)
	{
		___vsComGote_22 = value;
	}

	inline static int32_t get_offset_of_pushButtonBool_23() { return static_cast<int32_t>(offsetof(GameManager_t2252321495, ___pushButtonBool_23)); }
	inline bool get_pushButtonBool_23() const { return ___pushButtonBool_23; }
	inline bool* get_address_of_pushButtonBool_23() { return &___pushButtonBool_23; }
	inline void set_pushButtonBool_23(bool value)
	{
		___pushButtonBool_23 = value;
	}
};

struct GameManager_t2252321495_StaticFields
{
public:
	// System.String GameManager::JOSEKIPATH
	String_t* ___JOSEKIPATH_12;

public:
	inline static int32_t get_offset_of_JOSEKIPATH_12() { return static_cast<int32_t>(offsetof(GameManager_t2252321495_StaticFields, ___JOSEKIPATH_12)); }
	inline String_t* get_JOSEKIPATH_12() const { return ___JOSEKIPATH_12; }
	inline String_t** get_address_of_JOSEKIPATH_12() { return &___JOSEKIPATH_12; }
	inline void set_JOSEKIPATH_12(String_t* value)
	{
		___JOSEKIPATH_12 = value;
		Il2CppCodeGenWriteBarrier(&___JOSEKIPATH_12, value);
	}
};

#ifdef __clang__
#pragma clang diagnostic pop
#endif
