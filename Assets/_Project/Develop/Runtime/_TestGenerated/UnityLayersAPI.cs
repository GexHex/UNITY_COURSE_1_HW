using UnityEngine;

public static class UnityLayers
{
	public static readonly int LayerDefault = LayerMask.NameToLayer("Default");
	public static readonly int LayerTransparentFX = LayerMask.NameToLayer("TransparentFX");
	public static readonly int LayerIgnoreRaycast = LayerMask.NameToLayer("Ignore Raycast");
	public static readonly int LayerWater = LayerMask.NameToLayer("Water");
	public static readonly int LayerUI = LayerMask.NameToLayer("UI");
	public static readonly int LayerTest1 = LayerMask.NameToLayer("Test1");
	public static readonly int LayerTest2 = LayerMask.NameToLayer("Test2");
	public static readonly int LayerTest3 = LayerMask.NameToLayer("Test3");

	public static readonly int LayerMaskDefault = 1 << LayerDefault;
	public static readonly int LayerMaskTransparentFX = 1 << LayerTransparentFX;
	public static readonly int LayerMaskIgnoreRaycast = 1 << LayerIgnoreRaycast;
	public static readonly int LayerMaskWater = 1 << LayerWater;
	public static readonly int LayerMaskUI = 1 << LayerUI;
	public static readonly int LayerMaskTest1 = 1 << LayerTest1;
	public static readonly int LayerMaskTest2 = 1 << LayerTest2;
	public static readonly int LayerMaskTest3 = 1 << LayerTest3;
}
