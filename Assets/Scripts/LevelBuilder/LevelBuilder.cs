using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [SerializeField] private SpawnArea _spawner;

    [SerializeField] private Insect _commonAntPrefab;
    [SerializeField] private Insect _advancedAntPrefab;
    [SerializeField] private Insect _commonFlyPrefab;
    [SerializeField] private Insect _commonWaspPrefab;
    [SerializeField] private Insect _superAntPrefab;

    private Level _level;
    private TextAsset _file;
    private string _fileName = "Levels/";

    public SpawnArea SpawnArea => _spawner;

    public void LoadLevel(int levelId)
    {
        if(_file = Resources.Load<TextAsset>(_fileName + levelId.ToString()))
        {
            _level = JsonUtility.FromJson<Level>(_file.text);
            _spawner.gameObject.SetActive(true);
            _spawner.SetInsectList(LoadInsectCounts());
        } 
        else
        {
            PlayerPrefs.SetInt("LevelId", 1);
        }
    }

    private InsectList[] LoadInsectCounts()
    {
        InsectList commonAntList = new InsectList(_commonAntPrefab, _level.CommonAntCount);
        InsectList advancedAntList = new InsectList(_advancedAntPrefab, _level.CommonAntCount);
        InsectList commonFlyList = new InsectList(_commonFlyPrefab, _level.CommonAntCount);
        InsectList commonWaspList = new InsectList(_commonWaspPrefab, _level.CommonAntCount);
        InsectList superAntList = new InsectList(_superAntPrefab, _level.CommonAntCount);

        return new InsectList[] { commonAntList, advancedAntList, commonFlyList, commonWaspList, superAntList };
    }
}
