using MetroidMod;
using MetroidMod.Common;
using static MetroidMod.Common.Configs.MConfigMain;
using MetroidMod.Common.Players;
using MetroidMod.ID;
using MetroidMod.Content.SuitAddons;
using MetroidMod.Content.Buffs;
using static MetroidMod.ModSuitAddon;
using static MetroidMod.MUtils;
using CalamityMod.Items;
using CalamityMod.Items.Materials;
using CalamityMod.Tiles.Furniture.CraftingStations;
using CalamityMod.CalPlayer;
using static CalamityMod.CalPlayer.Dashes.CounterScarfDash;
using static CalamityMod.CalPlayer.CalamityPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using System.Security.Cryptography.X509Certificates;

namespace MetroidCalamity.Content.SuitAddons
{
	public class CosmiliteAugment : ModSuitAddon
	{
		public override string ItemTexture => $"{Mod.Name}/Assets/Textures/SuitAddons/CosmiliteAugment/CosmiliteAugmentItem";

		public override string TileTexture => $"{Mod.Name}/Assets/Textures/SuitAddons/CosmiliteAugment/CosmiliteAugmentTile";

		public override string ArmorTextureHead => $"{Mod.Name}/Assets/Textures/SuitAddons/CosmiliteAugment/CosmiliteAugmentHelmet_Head";

		public override string ArmorTextureTorso => $"{Mod.Name}/Assets/Textures/SuitAddons/CosmiliteAugment/CosmiliteAugmentBreastplate_Body";

		public override string ArmorTextureLegs => $"{Mod.Name}/Assets/Textures/SuitAddons/CosmiliteAugment/CosmiliteAugmentGreaves_Legs";

		public override bool AddOnlyAddonItem => false;

		public override bool CanGenerateOnChozoStatue() => MetroidMod.Common.Configs.MConfigMain.Instance.drunkWorldHasDrunkStatues || NPC.downedMoonlord;
		public override double GenerationChance() => 1;

		//This is where all of the suit addon's stats are stored.
		//They're outside a method so it can be directly accessed by the localization.
		//Put in the numbers like they'd be seen on the tooltip. The values are automatically adjusted for the actual stats.
		public static int suitDef = 42; //Added suit defense
		public static int energyCap = 6; //Added E-tank capacity
		public static float energyEff = 40f; //%Increased energy damage absorption
		public static float energyRes = 37.5f; //%Increased energy DR
		public static int overheatCap = 100; //Added maximum overheat
		public static float overheatCost = 25f; //%Decreased overheat cost
		public static float comboCost = 25f; //%Decreased Charge Combo cost
		public static float huntDamage = 15f; //%Increased hunter damage
		public static int huntCrit = 23; //Increased hunter crit
		public static float speedUp = 20f; //%Increased movement speed

		public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(suitDef, energyCap, energyEff, energyRes, overheatCap, overheatCost, comboCost, huntDamage, huntCrit, speedUp);

		public override void SetStaticDefaults()
		{
			ItemID.Sets.ShimmerTransformToItem[ItemType] = SuitAddonLoader.GetAddon<CosmiliteAugment>().ItemType;
			AddonSlot = SuitAddonSlotID.Suit_Primary;
			ItemNameLiteral = true;
		}
		public override void SetItemDefaults(Item item)
		{
			item.width = 16;
			item.height = 16;
			item.value = Item.buyPrice(2, 15, 60, 0);
			item.rare = ItemRarityID.Purple;
		}
		public override void OnUpdateArmorSet(Player player, int stack)
		{
			// Chromatic cloak ability
			if (!player.controlDownHold)
			{
				player.shimmerImmune = true;
			}

			// Ignore shimmer slowdown ability
			if(player.TryGetModPlayer(out IgnoreShimmerModPlayer shimmerMp))
			{
				shimmerMp.ignoreShimmer = true;
			}

			player.statDefense += suitDef;
			player.noKnockback = true;
			player.ignoreWater = true;
			if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
			{
				player.gills = true;
			}
			player.moveSpeed += speedUp / 100;
			player.lavaMax += 850;
			player.gravity = Player.defaultGravity;
			player.buffImmune[BuffID.VortexDebuff] = true;
			player.buffImmune[Terraria.ModLoader.ModContent.BuffType<MetroidMod.Content.Buffs.GravityDebuff>()] = true;
			MPlayer mp = player.GetModPlayer<MPlayer>();
			HunterDamagePlayer.ModPlayer(player).HunterDamageMult += huntDamage / 100;
			HunterDamagePlayer.ModPlayer(player).HunterCrit += huntCrit;
			mp.tankCapacity += energyCap;
			mp.maxOverheat += overheatCap;
			mp.overheatCost -= overheatCost / 100;
			mp.missileCost -= comboCost / 100;
			mp.EnergyDefenseEfficiency += energyEff / 100;
			mp.EnergyExpenseEfficiency += energyRes / 100;
			mp.UACost -= 0.30f;
			mp.accessHyperBeam = true;
			mp.phazonImmune = true;
			mp.accessPhazonBeam = true;}
		public override void OnUpdateVanitySet(Player player)
		{
			player.GetModPlayer<MPlayer>().visorGlowColor = new Color(196, 67, 45);
		}
		public override void ArmorSetShadows(Player player)
		{
			player.armorEffectDrawShadow = true;
		}
		public override void AddRecipes()
		{
			if (MUtils.CalamityActive())
				if (ModContent.TryFind("CalamityMod", "CosmiliteBar", out ModItem CosmiliteBar))
			if (MUtils.CalamityActive())
				if (ModContent.TryFind("CalamityMod", "IronBoots", out ModItem IronBoots))
			CreateRecipe(1)
				.AddIngredient(CosmiliteBar.Type, 36)
				.AddSuitAddon<VortexAugment>(1)
				.AddSuitAddon<NebulaAugment>(1)
				.AddIngredient(IronBoots.Type, 1)
                .AddTile(TileID.LunarCraftingStation)
				// my code ass tbh -lunaxio
				.Register();
		}
	}
}
