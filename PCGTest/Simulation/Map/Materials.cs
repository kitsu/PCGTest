using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCGTest.Simulation.Map
{
    class Materials
    {
        public Dictionary<string, int> AllIds;
        public Dictionary<int, string> AllNames;
        Dictionary<string, int> _pitIds;
        Dictionary<int, string> _pitNames;
        Dictionary<string, int> _floorIds;
        Dictionary<int, string> _floorNames;
        Dictionary<string, int> _wallIds;
        Dictionary<int, string> _wallNames;

        private static readonly Materials instance = new Materials();

        private Materials()
        {
            int index = 1;
            index = InitOther(index);
            index = InitPits(index);
            index = InitFloors(index);
            index = InitWalls(index);
        }

        int InitOther(int index)
        {
            AllIds = new Dictionary<string, int>
            {
                {"Void", 0 },
                {"Beach", index },
            };
            AllNames = new Dictionary<int, string>();
            int id;
            string name;
            foreach (var pair in AllIds)
            {
                name = pair.Key;
                id = pair.Value;
                AllNames[id] = name;
            }
            return index + 1;
        }

        int InitPits(int index)
        {
            // Increment index for each type, left shift 8+n for each sub-type
            _pitIds = new Dictionary<string, int>
            {
                {"FreshWater.Brick", (index + 0) },
                {"FreshWater.Rock", (index + 0) << 8 },
                {"FreshWater.Smooth", (index + 0) << 9 },
                {"SaltWater.Brick", (index + 1) },
                {"SaltWater.Rock", (index + 1) << 8 },
                {"SaltWater.Smooth", (index + 1) << 9 },
                {"PoisonWater.Brick", (index + 2) },
                {"PoisonWater.Rock", (index + 2) << 8 },
                {"PoisonWater.Smooth", (index + 2) << 9 },
                {"Lava", (index + 3) },
            };
            _pitNames = new Dictionary<int, string>();
            int id;
            string name;
            foreach (var pair in _pitIds)
            {
                name = pair.Key;
                id = pair.Value;
                AllIds["Pit." + name] = id;
                AllNames[id] = "Pit." + name;
                _pitNames[id] = name;
            }
            return index + 4;
        }

        int InitFloors(int index)
        {
            _floorIds = new Dictionary<string, int>
            {
                {"Void", 0 },
                {"Beach", (index + 0) },
                {"Sand", (index + 1) },
                {"Sand.Grass", (index + 1) << 8 },
                {"Sand.Gravel", (index + 1) << 9 },
                {"Sand.Farm", (index + 1) << 10 },
                {"Dirt", (index + 2) },
                {"Dirt.Grass", (index + 2) << 8 },
                {"Dirt.Gravel", (index + 2) << 9 },
                {"Dirt.Farm", (index + 2) << 10 },
                {"Mud", (index + 3) },
                {"Mud.Grass", (index + 3) << 8 },
                {"Mud.Gravel", (index + 3) << 9 },
                {"Mud.Farm", (index + 3) << 10 },
                {"Waste", (index + 4) },
                {"Waste.Grass", (index + 4) << 8 },
                {"Waste.Gravel", (index + 4) << 9 },
                {"Waste.Farm", (index + 4) << 10 },
                {"Brick.LiteBlue", (index + 5) << 8 },
                {"Brick.LiteGray", (index + 5) << 9 },
                {"Brick.DarkGray", (index + 5) << 10 },
                {"Brick.DarkBlue", (index + 5) << 11 },
                {"Wood", (index + 6) },
            };
            _floorNames = new Dictionary<int, string>();
            int id;
            string name;
            foreach (var pair in _floorIds)
            {
                name = pair.Key;
                id = pair.Value;
                AllIds["Floor." + name] = id;
                AllNames[id] = "Floor." + name;
                _floorNames[pair.Value] = pair.Key;
            }
            return index + 6;
        }

        int InitWalls(int index)
        {
            _wallIds = new Dictionary<string, int>
            {
                {"Void", 0 },
                {"Rock.Blue", (index + 0) },
            };
            _wallNames = new Dictionary<int, string>();
            int id;
            string name;
            foreach (var pair in _wallIds)
            {
                name = pair.Key;
                id = pair.Value;
                AllIds["Wall." + name] = id;
                AllNames[id] = "Wall." + name;
                _wallNames[id] = pair.Key;
            }
            return index + 1;
        }

        static public int GetPit(string name) => 
            instance._pitIds.ContainsKey(name) ? instance._pitIds[name] : 0;
        static public string GetPit(int id) =>
            instance._pitNames.ContainsKey(id) ? instance._pitNames[id] : "";
        static public int GetFloor(string name) => 
            instance._floorIds.ContainsKey(name) ? instance._floorIds[name] : 0;
        static public string GetFloor(int id) =>
            instance._floorNames.ContainsKey(id) ? instance._floorNames[id] : "";
        static public int GetWall(string name) => 
            instance._wallIds.ContainsKey(name) ? instance._wallIds[name] : 0;
        static public string GetWall(int id) =>
            instance._wallNames.ContainsKey(id) ? instance._wallNames[id] : "";
        static public int GetId(string name) => 
            instance.AllIds.ContainsKey(name) ? instance.AllIds[name] : 0;
        static public string GetName(int id) =>
            instance.AllNames.ContainsKey(id) ? instance.AllNames[id] : "";
    }
}
