package xmlrpc_serverapplication;

import java.io.IOException;
import org.apache.xmlrpc.XmlRpcException;
import org.apache.xmlrpc.server.PropertyHandlerMapping;
import org.apache.xmlrpc.server.XmlRpcServerConfigImpl;
import org.apache.xmlrpc.webserver.WebServer;

/**
 *
 * @author nivan
 */
public class XmlRpc_ServerApplication 
{
    public static void main(String[] args) throws XmlRpcException, IOException 
    {
        System.out.println("Starting XML-RPC server");
        WebServer server = new WebServer(1132);
        
        PropertyHandlerMapping phm = new PropertyHandlerMapping();
        phm.addHandler("Dhmz", Dhmz.class);
        server.getXmlRpcServer().setHandlerMapping(phm);
        
        XmlRpcServerConfigImpl serverConfigImpl = (XmlRpcServerConfigImpl)server.getXmlRpcServer().getConfig();
        serverConfigImpl.setEnabledForExtensions(true);
        
        server.start();
        System.out.println("Succesfull XML-RPC server start!");
        System.out.println("Request awaiting...");
    }
}
