using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using Org.BouncyCastle.Crypto.Paddings;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace OpcUaTest_DK
{

    public partial class frmMain : Form
    {
        public Session session;
        public string endpointURL;

        private Opc.Ua.ApplicationConfiguration config;
        private MonitoredItem monitoredItem;
        private Subscription subscription;

        public async Task OpcuaStart(string endPointUrl)
        {
            try
            {
                // Config
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Step 1 - Create a config.");
                config = CreateOpcUaAppConfiguration();

                // Create Session
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Step 2 - Create a session.");
                session = await Session.Create(config, new ConfiguredEndpoint(null, new EndpointDescription(endPointUrl)),
                    true,
                    "OPC UA Client",
                    60000,
                    null,
                    null);

                // Browse Session
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Step 3 - Browse the server namespace.");
                ReferenceDescriptionCollection references;
                Byte[] continuationPoint;
                session.Browse(
                    null,
                    null,
                    ObjectIds.ObjectsFolder,
                    0u,
                    BrowseDirection.Forward,
                    ReferenceTypeIds.HierarchicalReferences,
                    true,
                    (uint)NodeClass.Object | (uint)NodeClass.Variable,
                    out continuationPoint,
                    out references);

                // Draw TreeView
                foreach (var rd in references)
                {
                    // Parents Node 생성
                    TreeNode item = new TreeNode(rd.DisplayName.Text);
                    // Parents Note의 태그 값은 빈 값
                    item.Tag = "";
                    ReferenceDescriptionCollection childReferences;
                    byte[] nextContinuationPoint;
                    session.Browse(
                        null,
                        null,
                        ExpandedNodeId.ToNodeId(rd.NodeId, session.NamespaceUris),
                        0u,
                        BrowseDirection.Forward,
                        ReferenceTypeIds.HierarchicalReferences,
                        true,
                        (uint)NodeClass.Object | (uint)NodeClass.Variable | (uint)NodeClass.Method,
                        out nextContinuationPoint,
                        out childReferences);
                    foreach (var nextRd in childReferences)
                    {
                        // Child Node 생성
                        TreeNode childItem = new TreeNode(nextRd.DisplayName.Text);
                        // Child Note의 태그 값은 NodeId 값으로 설정
                        childItem.Tag = ExpandedNodeId.ToNodeId(nextRd.NodeId, session.NamespaceUris).ToString();
                        // Parents Node에 Child Node 추가
                        item.Nodes.Add(childItem);
                    }
                    treeViewNS.Nodes.Add(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " " + e.ToString());
                return;
            }
        }

        public async Task OpcuaEnd()
        {
            if (session != null)
            {
                try
                {
                    await Task.Run(() => session.Close());
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Disconnecting from OPC UA Server...");

                    session.Dispose();
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Disposing from OPC UA Server...");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " " + e.ToString());
                }
                finally
                {
                    session = null;
                }
            }
        }

        private Opc.Ua.ApplicationConfiguration CreateOpcUaAppConfiguration()
        {
            var config = new Opc.Ua.ApplicationConfiguration()
            {
                ApplicationName = "OPC UA Client",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration
                {
                    ApplicationCertificate = new CertificateIdentifier(),
                    // 신뢰할 수 없는 인증서 허용
                    AutoAcceptUntrustedCertificates = true
                },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 }
            };

            config.Validate(ApplicationType.Client);

            // 신뢰할 수 없는 인증서 허용
            if (config.SecurityConfiguration.AutoAcceptUntrustedCertificates)
            {
                config.CertificateValidator.CertificateValidation += (s, e) =>
                 {
                     e.Accept = true;
                     Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Untrusted Certificate Accepted.");
                 };
            }
            return config;
        }

        // Fix for CS0029 and CS8600 errors in the OnNotification method.
        private void OnNotification(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {
            // Ensure the NotificationValue is of type MonitoredItemNotification and handle potential null values.
            if (e.NotificationValue is MonitoredItemNotification notification)
            {
                foreach (var value in item.DequeueValues())
                {
                    Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" +
                                      $" Notification: {item.DisplayName}, Value: {value.Value}, Timestamp: {value.SourceTimestamp}");
                }
            }
            else
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} NotificationValue is not of type MonitoredItemNotification or is null.");
            }
        }

        // 단일 Node 모니터링 시 사용
        private void monitoredItem_Notification(MonitoredItem monitoredItem, MonitoredItemNotificationEventArgs e)
        {
            if (e.NotificationValue is MonitoredItemNotification notification)
            {
                if (notification == null) return;
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" +
                                      $" Notification: {monitoredItem.DisplayName}, Value: {notification.Value}");
            }
            else
            {
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} NotificationValue is not of type MonitoredItemNotification or is null.");
            }
        }

        public frmMain()
        {
            InitializeComponent();

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            endpointURL = "opc.tcp://" + txtIpPort.Text;
            _ = OpcuaStart(endpointURL);
        }


        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        private void Form1_Load(object sender, EventArgs e)
        {
            AllocConsole();
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}" + " Console Started");
        }

        private async void btnDisconnect_Click(object sender, EventArgs e)
        {
            await OpcuaEnd();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            try
            {
                // Read the value from the specified NodeId
                DataValue readingValue = ReadValue(new NodeId(txtNodeId.Text));

                lblReadingValue.Text = readingValue.ToString();

                // Log the value to the console
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} NodeId - {txtNodeId.Text} Value: {readingValue.Value}");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the read operation
                Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} Error reading NodeId - {txtNodeId.Text}: {ex.Message}");
            }
        }

        public DataValue ReadValue(NodeId nodeId)
        {
            // RequestHeader 생성 및 RequestHandle 설정
            var requestHeader = new RequestHeader
            {
                Timestamp = DateTime.UtcNow,
                RequestHandle = (uint)new Random().Next(1, int.MaxValue)
            };

            // ReadValueId 생성
            var readValueId = new ReadValueId
            {
                NodeId = nodeId,
                AttributeId = Attributes.Value
            };

            // Read 실행
            DataValueCollection results;
            DiagnosticInfoCollection diagnosticInfos;
            session.Read(
                requestHeader,
                0,
                TimestampsToReturn.Both,
                new ReadValueIdCollection { readValueId },
                out results,
                out diagnosticInfos);

            // RequestHandle 출력
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} Read RequestHandle: {requestHeader.RequestHandle}");
            lblReqHandle.Text = requestHeader.RequestHandle.ToString();
            return results[0];
        }
    }
}
 