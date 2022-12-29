#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.UInt32 Opsive.Shared.Inventory.IItemCategoryIdentifier::get_ID()
// 0x00000002 System.Collections.Generic.IReadOnlyList`1<Opsive.Shared.Inventory.IItemCategoryIdentifier> Opsive.Shared.Inventory.IItemCategoryIdentifier::GetDirectParents()
// 0x00000003 System.UInt32 Opsive.Shared.Inventory.IItemIdentifier::get_ID()
// 0x00000004 Opsive.Shared.Inventory.ItemDefinitionBase Opsive.Shared.Inventory.IItemIdentifier::GetItemDefinition()
// 0x00000005 System.Void Opsive.Shared.Inventory.ItemDefinitionBase::.ctor()
extern void ItemDefinitionBase__ctor_mEE56BE1DBA20B03FD78C2F75BD803310273BBD40 (void);
// 0x00000006 Opsive.Shared.Inventory.IItemIdentifier Opsive.Shared.Inventory.ItemDefinitionBase::CreateItemIdentifier()
// 0x00000007 Opsive.Shared.Inventory.ItemDefinitionBase Opsive.Shared.Inventory.ItemDefinitionBase::GetParent()
// 0x00000008 Opsive.Shared.Inventory.IItemCategoryIdentifier Opsive.Shared.Inventory.ItemDefinitionBase::GetItemCategory()
static Il2CppMethodPointer s_methodPointers[8] = 
{
	NULL,
	NULL,
	NULL,
	NULL,
	ItemDefinitionBase__ctor_mEE56BE1DBA20B03FD78C2F75BD803310273BBD40,
	NULL,
	NULL,
	NULL,
};
static const int32_t s_InvokerIndices[8] = 
{
	0,
	0,
	0,
	0,
	6052,
	0,
	0,
	0,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_Opsive_Shared_Inventory_CodeGenModule;
const Il2CppCodeGenModule g_Opsive_Shared_Inventory_CodeGenModule = 
{
	"Opsive.Shared.Inventory.dll",
	8,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
