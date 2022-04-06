package jaxbproject;

import java.io.File;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Scanner;
import javax.xml.XMLConstants;
import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;
import javax.xml.validation.Schema;
import javax.xml.validation.SchemaFactory;
import org.datacontract.schemas._2004._07.soapservis.Hotels;
import org.xml.sax.SAXException;



/**
 *
 * @author nivan
 */
public class JAXBProject 
{
    public static void main(String[] args) throws IOException 
    {
        ServerSocket serverSocket = new ServerSocket(1133,10);
        System.out.println("Sever is ready...");
        
        Socket client = serverSocket.accept();
        Scanner scanner = new Scanner(client.getInputStream());
        PrintWriter pw = new PrintWriter(client.getOutputStream(), true);
        String xsdFile = "C:\\Users\\nivan\\Desktop\\Algebra\\Projects\\NevenIvanisic_IIS.21\\hotels_shema.xsd";
        
        String XmlFilename = scanner.nextLine();
        JAXBContext jaxbContext;
        
        try 
        {
            jaxbContext = JAXBContext.newInstance(Hotels.class);
            
            Unmarshaller jaxbUnmarshaller = jaxbContext.createUnmarshaller();
            
            SchemaFactory sf = SchemaFactory.newInstance(XMLConstants.W3C_XML_SCHEMA_NS_URI);
            Schema hotelsSchema = sf.newSchema(new File(xsdFile));
            jaxbUnmarshaller.setSchema(hotelsSchema);
            
            Hotels hotels = (Hotels) jaxbUnmarshaller.unmarshal(new File(XmlFilename));
            
            System.out.println(hotels.getHotelDataList().getHotelData().toString());
            System.out.println("Xml datoteka je ispravna.");
            pw.println("Xml datoteka je ispravna.");
            
        } 
        catch (JAXBException | SAXException e) 
        {
            System.out.println("Xml datoteka nije ispravna");
            pw.println("Xml datoteka nije ispravna");
            
            e.printStackTrace();
        }
        
        client.close();
    }
    
}
