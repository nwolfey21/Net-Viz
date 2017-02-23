using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Node nodePrefab;
	public Link linkPrefab;
	public Text statusText;
	public Text nodeCountText;
	public Text linkCountText;

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

        StartCoroutine(LoadLayout());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	//Method for loading the GraphML layout file
	private IEnumerator LoadLayout(){
        int numNodes = 10;
/*		string sourceFile = Application.dataPath + "/Data/layout.xml";
		statusText.text = "Loading file: " + sourceFile;

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
*/
		statusText.text = "Loading Topology";

		int scale = 2;

        for (int i = 0; i < numNodes; i++) {
            for (int j = 0; j < numNodes; j++) {
				float x = i * 3;
                float y = 1;
                float z = 1;

                Node nodeObject = Instantiate(nodePrefab, new Vector3(x, y, z), Quaternion.identity) as Node;
                nodeObject.nodeText.text = string.Format("node {0:d}", i);

                nodeObject.id = i++.ToString();
                nodes.Add(nodeObject.id, nodeObject);

                statusText.text = "Loading Topology: Node " + nodeObject.id;
                nodeCount++;
                nodeCountText.text = "Nodes: " + nodeCount;

                //every 100 cycles return control to unity
                if (j % 100 == 0)
                    yield return true;
            }
        }
				

/*		XmlElement root = xmlDoc.FirstChild as XmlElement;
		for(int i=0; i<root.ChildNodes.Count; i++){
			XmlElement xmlGraph = root.ChildNodes[i] as XmlElement;

			for(int j=0; j<xmlGraph.ChildNodes.Count; j++){
				XmlElement xmlNode = xmlGraph.ChildNodes[j] as XmlElement;

				//create nodes
				if(xmlNode.Name == "node"){
					float x = float.Parse(xmlNode.Attributes["x"].Value)/scale;
					float y = float.Parse (xmlNode.Attributes["y"].Value)/scale;
					float z = float.Parse(xmlNode.Attributes["z"].Value)/scale;

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
*/	}

	//Method for mapping links to nodes
	private void MapLinkNodes(){
		foreach(string key in links.Keys){
			Link link = links[key] as Link;
			link.source = nodes[link.sourceId] as Node;
			link.target = nodes[link.targetId] as Node;
		}
	}
}