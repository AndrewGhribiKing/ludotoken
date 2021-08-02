using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class ludoTokenClass : MonoBehaviour
{

// Top level token specific data returned by the API - do not change this if you are serializing straight from the API.
// Contains a nested class for complex meta - this would be where the 'traits' or your own custom data comes in.
// Contains a nested class for owner specific data.
// contains a nested class for local data - this could be for linking a local texture file to the token for example;

[Serializable]
public class Token
{
    [Header("Top level meta-data")]
    [Space]
    public string tokenId;
    public string policyId;
    public string createdAt;
    public complexMeta meta;
    public ownerMeta owner;
    public localData local;
}

// Complex meta data - you would customise this section according to the specific data your token contains.
// This data is only returned when requested for a specific token.
[Serializable]
public class complexMeta
{
    [Header("Complex meta-data")]
    [Space]
    public string type;
    public string colour;
    public string variant;
    public int level;
}

// Owner meta data - do not change this if you are serializing straight from the API.
// This data is only returned when requested for a specific token.
[Serializable]
public class ownerMeta
{
    [Header("Owner meta-data")]
    [Space]
    public string address;
    public string stakeId;
    public string acquisitionDate;

}

// local data - change this up to whatever asscociated local data you want to store against tokens.
[Serializable]
public class localData
{
    [Header("Local data")]
    [Space]
    public Texture tokenLabel;

}

// A list of tokens - this list is populated with top level data in one call to the API.
// Once this list is populated, you can request specific token data to return the rest - recommend you use a local data set where possible for this.
[Serializable]
public class tokenList
{
    [TextArea]
    public string Notes = "This list of tokens is the result of parsing our Json data into the LudoToken class.";
    [Space]
    public string policyId;
    public Token[] token;
}
}