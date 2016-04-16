﻿using System.Collections.Generic;
using System.IO;
using DevilMind;
using Newtonsoft.Json;

namespace Assets.Scripts.Gameplay.Settings
{
    public class GameSave
    {
        public GameSave()
        {
            LoadSaveSlots();
            LoadGame(1);
        }

        public Save CurrentSave { private set; get; }

        public readonly List<Save> SaveSlots = new List<Save>(); 

        public class Save
        {
            public readonly int SlotNumber;
            public int StageNumber;
            public int Checkpoint;

            public Save(int slotNumber)
            {
                SlotNumber = slotNumber;
                StageNumber = 1;
                Checkpoint = 1;
            }
        }

        public Save LoadGame(int slotNumber)
        {
            if (CurrentSave == null)
            {
                if (SaveSlots.Count == 0)
                {
                    SaveSlots.Add(new Save(slotNumber));
                    SaveCurrentGameProgress();
                }
                CurrentSave = SaveSlots[0];
            }
            return CurrentSave;
//TODO WHEN MENU WILL BE READY CREATE SAVE SLOTS FROM MENU
//            if (SaveSlots.Count < slotNumber && SaveSlots[slotNumber] != null)
//            {
//                CurrentSave = SaveSlots[slotNumber];
//                return SaveSlots[slotNumber];
//            }
//
//            return null;
        }

        public void SaveCurrentGameProgress()
        {
            var saveInText = JsonConvert.SerializeObject(SaveSlots, Formatting.Indented);
            File.WriteAllText("Assets/Resources/GameStateSave.txt", saveInText);
        }

        private void LoadSaveSlots()
        {
            var textAsset = ResourceLoader.LoadGameSave();
            if (textAsset == null)
            {
                Log.Error(MessageGroup.Common, "Cant load game saves file!");
                return;
            }

            if (string.IsNullOrEmpty(textAsset.text))
            {
                return;
            }

            var slotList = JsonConvert.DeserializeObject<List<Save>>(textAsset.text);
            if (slotList != null)
            {
                slotList.Sort((slot, save1) => slot.SlotNumber < save1.SlotNumber ? 1 : -1);
                SaveSlots.Clear();
                SaveSlots.AddRange(slotList);
            }
        }
    }
}