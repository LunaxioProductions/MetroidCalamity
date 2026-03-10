using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MetroidCalamity.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class AlatreonGreatsword : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.MetroidCalamity.hjson' file.
		public override void SetDefaults()
		{
			Item.damage = 800;
			Item.DamageType = DamageClass.Melee;
			Item.width = 802;
			Item.height = 802;
			Item.useTime = 15;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 10);
			Item.rare = ItemRarityID.Purple;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Zenith, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
