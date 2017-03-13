using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Xml;

public class GameController : MonoBehaviour {

	public Node nodePrefab;
	public Link linkPrefab;
	public Text statusText;
	public Text nodeCountText;
	public Text linkCountText;

	private int scale = 10;
	private float dr = 3.0f;
	private float dh = 20.0f;

	//Parameters for k36 3level fat tree
	private int numTerminals = 3564;
	private int numLevel2 = 108;
	private int numLevel1 = 198;
	private int numLevel0 = 198;
	private int numLevels = 3;


	//Parameters for k8 3level fat tree
/*	private int numTerminals = 48;
	private int numLevel2 = 8;
	private int numLevel1 = 12;
	private int numLevel0 = 12;
	private int numLevels = 3;
*/
	private Hashtable nodes;
	private Hashtable links;
	private int nodeCount = 0;
	private int linkCount = 0;
//	private GUIText nodeCountText;
//	private GUIText linkCountText;

	// Use this for initialization
	void Start () {
        nodes = new Hashtable();
        links = new Hashtable();

        //initial stats
//        nodeCountText = GameObject.Find("NodeCount").GetComponent<GUIText>();
        nodeCountText.text = "Nodes: 0";
//        linkCountText = GameObject.Find("LinkCount").GetComponent<GUIText>();
        linkCountText.text = "Edges: 0";
//        statusText = GameObject.Find("StatusText").GetComponent<GUIText>();
        statusText.text = "";

//        StartCoroutine(LoadLayoutNew());
		StartCoroutine(LoadLayout());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	//Method for loading the GraphML layout file
	private IEnumerator LoadLayoutNew(){
        int numNodes = 10;

		statusText.text = "Loading Topology";

		int idx = 0;

		//Create Nodes
        for (int i = 0; i < numNodes; i++) {
            for (int j = 0; j < numNodes; j++) {
				float x = i * 3;
				float y = j * 2 + 1;
                float z = 1;

				idx = i * numNodes + j;

                Node nodeObject = Instantiate(nodePrefab, new Vector3(x, y, z), Quaternion.identity) as Node;
				nodeObject.nodeText.text = string.Format("node {0:d}", i * numNodes + j);

                nodeObject.id = idx++.ToString();
				//print ("node id:" + nodeObject.id);
                nodes.Add(nodeObject.id, nodeObject);

                statusText.text = "Loading Topology: Node " + nodeObject.id;
                nodeCount++;
                nodeCountText.text = "Nodes: " + nodeCount;

				//Create Links
				if(nodeCount > 1){
					Link linkObject = Instantiate(linkPrefab, new Vector3(0,0,0), Quaternion.identity) as Link;
					linkObject.id = nodeObject.id;
					linkObject.sourceId = nodeObject.id;
					int target = idx - 2;
					linkObject.targetId = target.ToString();
					linkObject.status = "up";
					links.Add(linkObject.id, linkObject);

					statusText.text = "Loading Topology: Edge " + linkObject.id;
					linkCount++;
					linkCountText.text = "Edges: " + linkCount;
				}

                //every 100 cycles return control to unity
                if (j % 100 == 0)
                    yield return true;
            }
        }

		//map node edges
		MapLinkNodes();
		statusText.text = "Status: Chillin";
	}

	//Method for loading the GraphML layout file
	private IEnumerator LoadLayout(){
//		print ("application.datapath:" + Application.dataPath);
		string sourceFile = Application.dataPath + "/Data/fat-tree.xml";
//		string sourceFile = Application.dataPath + "/Data/layout.xml";
		statusText.text = "Loading file: " + sourceFile;

//		int scale = 25;

		//determine which platform to load for
		string xml = null;
		if(Application.isWebPlayer){
			WWW www = new WWW (sourceFile);
			yield return www;
			xml = www.text;
		}
		else{
			StreamReader sr = new StreamReader(sourceFile);
			xml = sr.ReadToEnd();
			sr.Close();
		}

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml(xml);

		//map node edges
		MapLinkNodes();
		statusText.text = "Status: Chillin";

		XmlElement root = xmlDoc.FirstChild as XmlElement;
		for(int i=0; i<root.ChildNodes.Count; i++){
			XmlElement xmlGraph = root.ChildNodes[i] as XmlElement;

			for(int j=0; j<xmlGraph.ChildNodes.Count; j++){
				XmlElement xmlNode = xmlGraph.ChildNodes[j] as XmlElement;

				//float level = -1.0f;
				float dTheta = 0.0f;
				float theta = 0.0f;
				int id = 0;
				//int idx = 0;
				//create nodes
				if(xmlNode.Name == "lp"){
/*					float x = float.Parse(xmlNode.Attributes["x"].Value)/scale;
					float y = float.Parse (xmlNode.Attributes["y"].Value)/scale;
					float z = float.Parse(xmlNode.Attributes["z"].Value)/scale;
*/
					//float level = -1.0f;

//					print ("here");
					int level = int.Parse(xmlNode.Attributes["level"].Value);
//					print ("level" + level);
					int idx = int.Parse(xmlNode.Attributes["idx"].Value);
					if (level == 10) {
						dTheta = (2.0f * Mathf.PI) / numTerminals;
						id = idx;
					}
					if (level == 0) {
						dTheta = (2.0f * Mathf.PI) / numLevel0;
						id = idx;
					}
					if (level == 1) {
						dTheta = (2.0f * Mathf.PI) / numLevel1;
						id = idx - numLevel0;
					}
					if (level == 2) {
						dTheta = (2.0f * Mathf.PI) / numLevel2;
						id = idx - numLevel0 - numLevel1;
					}

					float x, y, z;
					theta = id * dTheta;
					if (level < 3) {
						x = (float)(numLevels - level) * scale * dr * Mathf.Cos (theta);
						z = (float)(numLevels - level) * scale * dr * Mathf.Sin (theta);
						y = (float)(level + 1.0f) * dh;
					}else{
						x = ((float)numLevels+1.0f) * scale * dr * Mathf.Cos (theta);
						z = ((float)numLevels+1.0f) * scale * dr * Mathf.Sin (theta);
						y = 0.0f;
					}
							
					Node nodeObject = Instantiate(nodePrefab, new Vector3(x,y,z), Quaternion.identity) as Node;
					nodeObject.nodeText.text = xmlNode.Attributes["name"].Value;

					nodeObject.id = xmlNode.Attributes["id"].Value;
					nodes.Add(nodeObject.id, nodeObject);

					statusText.text = "Loading Topology: Node " + nodeObject.id;
					nodeCount++;
					nodeCountText.text = "Nodes: " + nodeCount;
				}

				//create links
				if(xmlNode.Name == "edge"){
					Link linkObject = Instantiate(linkPrefab, new Vector3(0,0,0), Quaternion.identity) as Link;
					linkObject.id = xmlNode.Attributes["id"].Value;
					linkObject.sourceId = xmlNode.Attributes["source"].Value;
					linkObject.targetId = xmlNode.Attributes["target"].Value;
					linkObject.status = xmlNode.Attributes["status"].Value;
					links.Add(linkObject.id, linkObject);

					statusText.text = "Loading Topology: Edge " + linkObject.id;
					linkCount++;
					linkCountText.text = "Edges: " + linkCount;
				}

				//every 100 cycles return control to unity
				if(j % 100 == 0)
					yield return true;
			}
		}

		//map node edges
		MapLinkNodes();

		statusText.text = "";
	}

	//Method for mapping links to nodes
	private void MapLinkNodes(){
		foreach(string key in links.Keys){
			Link link = links[key] as Link;
			link.source = nodes[link.sourceId] as Node;
			link.target = nodes[link.targetId] as Node;
			//print ("link.sourceId:" + link.sourceId + " link.targetId:" + link.targetId + " link.source:" + link.source + " link.target:" + link.target);
		}
	}
}