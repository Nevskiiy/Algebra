package xmlrpc_serverapplication;

import java.io.IOException;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLConnection;
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

/**
 *
 * @author nivan
 */
public class Dhmz 
{
    public double GetTemperature(String grad) throws ParserConfigurationException, MalformedURLException, IOException, SAXException
    {
        String url = "https://vrijeme.hr/hrvatska_n.xml";
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
        dbf.setNamespaceAware(false);
        DocumentBuilder db = dbf.newDocumentBuilder();
        URLConnection urlConnection = new URL(url).openConnection();
        urlConnection.addRequestProperty("Accept", "application/xml");
        Document document = (Document)db.parse(urlConnection.getInputStream());
        Element element = document.getDocumentElement();
        
        
        /* List of towns */
        NodeList nList = element.getElementsByTagName("Grad");
        if (nList != null && nList.getLength()> 0) 
        {
            for (int i = 0; i < nList.getLength(); i++) 
            {
                /* Data for town */
                NodeList subList = nList.item(i).getChildNodes();
                
                if (subList != null && subList.getLength() > 0) 
                {
                    if (subList.item(1).getFirstChild().getNodeValue().equals(grad)) 
                    {
                        return Double.parseDouble(subList.item(7).getChildNodes().item(1).getFirstChild().getNodeValue());
                    }
                }
            }
        }
        
        throw new IllegalArgumentException("There is no information for wanted town, choose another one.");
    }
}
