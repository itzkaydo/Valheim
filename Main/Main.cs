using UnityEngine;
using System.Collections.Generic;
using System.Reflection;

namespace Menu.Main
{
    public class Main : MonoBehaviour
    {
        public bool enableitemvac;
        public bool enablespeedhack;
        public static string itemName;
        //private static bool znetSet = false;
        public static bool showGui;
        private static bool toggleCheats = false;
        public static BindingFlags fieldBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField;
        bool showcoords = false;
        bool showitemesp = false;
        bool showplayeresp = false;
        bool godmode = false;
        bool fly = false;
        bool debug = false;
        string[] skills = { "swords", "knives", "clubs", "polearms", "spears", "blocking", "axes", "bows", "unarmed", "pickaxes", "woodcutting", "jump", "sneak", "run", "swim" };
        private void Start()
        {
        }

        private void Update()
        {
            /*if (ZNet.instance != null && !znetSet && !ZNet.instance.IsServer())
            {
                typeof(ZNet).GetField("m_isServer", fieldBindingFlags).SetValue(typeof(ZNet), true);
                znetSet = true;
            }*/

            if (Input.GetKeyDown(KeyCode.F11))
            {
                Skills();
            }
            if (Input.GetKeyDown(KeyCode.F1))
            {
                godmode = !godmode;
            }
            if (Input.GetKeyDown(KeyCode.Home))
            {
                enableitemvac = !enableitemvac;
                if (!enableitemvac)
                {
                    Player.m_localPlayer.m_autoPickupRange = 2;
                }
                else
                {
                    Player.m_localPlayer.m_autoPickupRange = 500;
                }

            }
            if (Input.GetKeyDown(KeyCode.Insert))
            {
                fly = !fly;
                if (!fly)
                {
                    Player.m_localPlayer.m_flying = false;
                }
                else
                {
                    Player.m_localPlayer.m_flying = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.PageUp))
            {
                enablespeedhack = !enablespeedhack;
                if (!enablespeedhack)
                {
                    Player.m_localPlayer.m_runSpeed = 7;
                }
                else
                {
                    Player.m_localPlayer.m_runSpeed = 20;
                }
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                toggleCheats = !toggleCheats;
                Console.instance.GetType().GetField("m_cheat", fieldBindingFlags).SetValue(Console.instance, toggleCheats);
                Player.m_debugMode = toggleCheats;
                debug = !debug;
                if (!debug)
                {
                    Player.m_debugMode = false;
                }
                else
                {
                    Player.m_debugMode = true;
                }
            }
            if (Input.GetKeyDown(KeyCode.F6))
            {
                showcoords = !showcoords;
            }
            if (Input.GetKeyDown(KeyCode.F3))
            {
                showplayeresp = !showplayeresp;
            }
            if (Input.GetKeyDown(KeyCode.F4))
            {
                showitemesp = !showitemesp;
            }
            if (Input.GetKeyDown(KeyCode.F7))
            {
                GameObject Eikthyr = GetEikthyr();
                if (Eikthyr == null)
                    return;
                GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Eikthyr, Player.m_localPlayer.transform.position + Player.m_localPlayer.transform.forward * 2, Player.m_localPlayer.transform.rotation);
            }
            if (Input.GetKeyDown(KeyCode.F9))
            {
                showGui = !showGui;
            }
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                ZoneSystem.LocationInstance locationInstance;
                ZoneSystem.instance.FindClosestLocation("Vendor_BlackForest", Player.m_localPlayer.transform.position, out locationInstance);
                Minimap.instance.DiscoverLocation(locationInstance.m_position, Minimap.PinType.Icon3, "Merchant");
                Console.instance.Print(string.Format("Merhcant is at : {0}", locationInstance.m_position));
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Loader.Unload();
            }
        }

        private void OnGUI()
        {
            {
                GUI.color = Color.white;
                GUI.Label(new Rect(10f, 15f, 350f, 20f), "ShieldSupporter Final");
                GUI.Label(new Rect(10f, 30f, 350f, 20f), "Max Skills - F11");

            }
            if (!showitemesp)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 90f, 350f, 20f), "Item ESP - F4");
            }
            else
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 90f, 350f, 20f), "Item ESP - F4");
            }
            if (!showplayeresp)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 75f, 350f, 20f), "ESP - F3");
            }
            else
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 75f, 350f, 20f), "ESP - F3");
            }
            if (!fly)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 105f, 350f, 20f), "Fly - Insert");
            }
            else
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 105f, 350f, 20f), "Fly - Insert");
            }
            if (!godmode)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 135f, 350f, 20f), "GodMode - F1");
            }
            else
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 135f, 350f, 20f), "GodMode - F1");
            }
            var cam = Utils.GetMainCamera();

            if (!cam)
                return;

            Player.m_localPlayer.m_maxCarryWeight = 13337;
            Player.m_localPlayer.m_blockStaminaDrain = 0;
            Player.m_localPlayer.m_runStaminaDrain = 0;
            Player.m_localPlayer.m_jumpStaminaUsage = 0;
            Player.m_localPlayer.m_sneakStaminaDrain = 0;
            Player.m_localPlayer.m_equipStaminaDrain = 0;
            Player.m_localPlayer.m_blockStaminaDrain = 0;
            Player.m_localPlayer.m_staminaRegen = 100;
            if (godmode)
            {
                Player.m_localPlayer.SetMaxHealth(100, false);
                Player.m_localPlayer.SetHealth(100);
            }
            if (Player.m_localPlayer.m_autoPickupRange == 2)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 45f, 350f, 20f), "ItemVAC - Home");
            }
            else if (Player.m_localPlayer.m_autoPickupRange == 500)
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 45f, 350f, 20f), "ItemVAC - Home");
            }
            if (Player.m_localPlayer.m_runSpeed == 7)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 60f, 350f, 20f), "Speed - PageUp");
            }
            else if (Player.m_localPlayer.m_runSpeed == 20)
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 60f, 350f, 20f), "Speed - PageUp");
            }
            if (Player.m_debugMode == false)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(10f, 120f, 350f, 20f), "Debug - F8");
            }
            else if (Player.m_debugMode == true)
            {
                GUI.color = Color.green;
                GUI.Label(new Rect(10f, 120f, 350f, 20f), "Debug - F8");
            }
                //GUI.Label(new Rect(Screen.width / 2, 10f, 350f, 50f), Player.m_debugMode.ToString());
            if (showGui)
            {
                itemName = GUI.TextField(new Rect(700f, 80f, 500f, 30f), itemName);
            }
            if (showcoords)
            {
                GUI.color = Color.white;
                GUI.Label(new Rect(Screen.width / 2, 10f, 350f, 50f), "X: " + Player.m_localPlayer.gameObject.transform.position.x.ToString());
                GUI.Label(new Rect(Screen.width / 2, 25f, 350f, 50f), "Y: " + Player.m_localPlayer.gameObject.transform.position.y.ToString());
                GUI.Label(new Rect(Screen.width / 2, 40f, 350f, 50f), "Z: " + Player.m_localPlayer.gameObject.transform.position.z.ToString());
            }
            var characters = Character.GetAllCharacters();
            foreach (Character character in characters)
            {
                var position = character.transform.position;
                var screen = cam.WorldToScreenPoint(position);
                var faction = character.m_faction;

                var color = Color.yellow;
                if (faction == Character.Faction.Players)
                {
                    if (Player.m_localPlayer == character)
                        continue;

                    color = Color.green;
                }
                else if (character.GetBaseAI().IsAlerted())
                {
                    color = Color.red;
                }
                if (showplayeresp)
                {
                    if (screen.x < 0f || screen.x > (float)Screen.width || screen.y < 0f || screen.y > (float)Screen.height || screen.z > 0f)
                    {
                        var distance = (int)(Vector2.Distance(Player.m_localPlayer.transform.position, character.transform.position));

                        screen.y = Screen.height - screen.y;

                        Render.Color = color;
                        Render.DrawString(new Vector2(screen.x, screen.y), character.m_name.Replace("$enemy_", ""));

                        Render.Color = Color.white;
                        Render.DrawString(new Vector2(screen.x, (screen.y) + 13), "" + distance.ToString() + "m");


                        var width = 25;
                        var health = character.GetHealth();
                        var maxHealth = character.GetMaxHealth();

                        screen.y += 23;

                        Render.Color = new Color(0, 0, 0);
                        Render.DrawBoxFill(new Vector2(screen.x - width / 2, screen.y + 1), new Vector2(width, 3), Render.Color);

                        Render.Color = new Color(0, 1, 0);
                        var health_width = new Vector2(health * width / maxHealth, 3);
                        Render.DrawBoxFill(new Vector2(screen.x - width / 2, screen.y + 1), health_width, Render.Color);

                        Render.Color = new Color(0, 0, 0);
                        Render.DrawBox(new Vector2(screen.x - width / 2, screen.y + 1), new Vector2(width, 3), 1, false);
                    }
                }
            }
            if (showitemesp)
            {
                Vector3 vector = Player.m_localPlayer.transform.position + Vector3.up;
                foreach (Collider collider in Physics.OverlapSphere(vector, 100f, LayerMask.GetMask("item")))
                {
                    if (collider.attachedRigidbody)
                    {
                        ItemDrop component = collider.attachedRigidbody.GetComponent<ItemDrop>();
                        if (!(component == null) && component.GetComponent<ZNetView>().IsValid())
                        {
                            var position = component.gameObject.transform.position;
                            var screen = cam.WorldToScreenPoint(position);

                            if (screen.x < 0f || screen.x > (float)Screen.width || screen.y < 0f || screen.y > (float)Screen.height || screen.z > 0f)
                            {
                                var distance = (int)(Vector2.Distance(Player.m_localPlayer.transform.position, position));

                                Render.Color = Color.gray;
                                Render.DrawString(new Vector2(screen.x, Screen.height - screen.y), component.name.Replace("(Clone)", ""));

                                Render.Color = Color.white;
                                Render.DrawString(new Vector2(screen.x, (Screen.height - screen.y) + 13), "" + distance.ToString() + "m");
                            }
                        }
                    }
                }
            }
        }
        public static GameObject GetEikthyr()
        {
            List<ZDO> itemlist = new List<ZDO>();
            ZDOMan.instance.GetAllZDOsWithPrefab(itemName, itemlist);
            foreach (ZDO zdo in itemlist)
            {
                if (zdo.IsValid())
                {
                    int prefab = zdo.GetPrefab();
                    if (prefab == 0)
                    {
                        continue;
                    }
                    return ZNetScene.instance.GetPrefab(prefab);
                }
            }
            return null;
        }
        public void Skills()
        {
            foreach (string value in skills)
            {
                Player.m_localPlayer.GetSkills().CheatRaiseSkill(value, 100);
            }
        }
    }
}
