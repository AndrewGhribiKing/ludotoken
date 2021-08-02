using UnityEngine;
using Newtonsoft.Json;
using System.IO;

/* code written for the LudoToken project by Andrew King with contributions from Adam K. Dean.
 * 
 * recommended top level workflow is as follows:
 * 
 * 1) load data to list (local or API)
 * 2) save data
 * 3) load local data to list (doing this resolves an inconsistent serialization issue with sub-classes when performing 5)
 * 
 * recommended token specific workflow is as follows:
 * 
 * 4) load individual token specific data to list
 * 5) append individual token data to list
 * 6) go to 2 or 4.
 * 
 * Loading the data from the API will overwrite the list. An improvement would be to create a second list of new data and compare it to the first, adding and removing items where necessary.
 */


public class tokendata : MonoBehaviour
{
    [Header("LUDOTOKEN")]
    [TextArea]
    public string description = "LudoToken is a super simple API that provides Json endpoints for retrieving token data from the Cardano BlockChain. LudoToken can be used with existing and well-documented methods in any game engine. LudoToken is a collaboration between Adam K.Dean and Andrew King";

    // create a bool and textasset for testing locally
    [Header("Local data")]
    [Tooltip("Turn on to prototype locally with a Json file")]
    public bool useLocalData;
    [Tooltip("Drop your local Json file here")]
    public TextAsset localDataFile;
    [Space]

    // ludoToken API project account Key - don't share with anyone!
    [Header("LudoToken Account Key")]
    [Tooltip("Your LudoToken account key so that we can drop your queries against it")]
    public string accountKey;
    [Space]

    // wallet ADDR we want to look in and the policy ID we want to look at. 
    [Header("Wallet and policy info")]
    [Tooltip("Drop wallet ADDR here")]
    public string walletADDR;
    [Tooltip("The Policy ID we want to look for")]
    public string policyID;
    [Space]

    // create array to hold our token data when loaded
    [Header("Token data")]
    [Tooltip("The results of our data serialization")]
    public ludoTokenClass.tokenList myTokenList = new ludoTokenClass.tokenList();

    // query API or load local data
    public void loadData()
    {
        if (useLocalData)
        {
            processJsonData(localDataFile.text);
        }
        else
        {
        // call ludoToken API (removed for now)
        }
    }

    // convert Json to a string and parse into MyTokenList
    public void processJsonData(string JSONText)
    {
        string jsonString = JSONText;
        // this method will overwrite all serialized data in the list - an advanced method could call this into a different list and amend local data accordingly.
        myTokenList = JsonConvert.DeserializeObject<ludoTokenClass.tokenList>(jsonString);
    }

    // load locally saved data if it exists
    public void loadLocalData()
    {
        string path = Application.persistentDataPath + "/saveTokenData.json";
        if (File.Exists(path))
        {
            //read save data and parse into our list - this will overwrite all data in the list.
            string data = File.ReadAllText(path);
            processJsonData(data);
        }
        else
        {
            Debug.Log("File does not exist");
        }
    }

    // save serialized data into a local JSON file - this will overwrite all current save data
    public void saveData()
    {
        //convert all serialized data in myTokenList to Json and save it.
        string saveData = JsonUtility.ToJson(myTokenList);
        File.WriteAllText(Application.persistentDataPath + "/saveTokenData.json", saveData);
    }

}
