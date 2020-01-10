using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;


namespace BPIM_API_Use___WinForms_Examples
{
    public partial class ConnectAndSweepForm : Form
    {
        private WebClient client;
        
        public ConnectAndSweepForm()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception e)
            {
                /// just ignore exceptions for now
            }
            client = new WebClient();
        }

        #region Form Events
        /// <summary>
        /// Event Handler Connect Button Clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (apiBaseUrl == "")
                try
                {
                    apiToken.Text = apiConnect(hostUrl.Text, hostPort.Text);
                    responseText.Enabled = true;
                    getSweep.Enabled = true;
                    btnConnect.Text = "Disconnect";
                }
                catch (Exception ex)
                {
                    apiToken.Text = ex.Message;
                }
            else
            {
                apiDisconnect();
                responseText.Enabled = false;
                getSweep.Enabled = false;
                btnConnect.Text = "Connect";
            }
        }

        private void getSweep_Click(object sender, EventArgs e)
        {
            apiSetTrigger();
            responseText.Text = apiGetSamples();
        }

        private void Form1_Leave(object sender, FormClosingEventArgs e)
        {
            apiDisconnect();
        }
        #endregion

        #region BPIM API Control

        private string apiBaseUrl;
        private string apiConnectToken;
        
        /// <summary>
        /// API Command is the guts of all communications with the BPIM Web Engine.
        /// Building a Uri from a base url path (i.e. http://localhost/api/) and an
        /// api Token retrieved using the apiGetToken method, this method appends a
        /// command as designated by the URL format in the PIM API Programmer's
        /// manual.
        /// 
        /// Prerequisites: The apiConnect method must be called before this method
        /// becomes useful (otherwise no apiBaseUrl or apiConnectToken will exist).
        /// </summary>
        /// <param name="command">Supported BPIM API command</param>
        /// <returns></returns>
        private Uri apiCommand(string command)
        {
            try
            {
                return new Uri(apiBaseUrl + "/" + apiConnectToken + "/" + command);
            }
            catch (UriFormatException e)
            {
                return null;
            }
        }

        /// <summary>
        /// apiConnect - This method initializes a connection to the BPIM Web Engine
        /// and obtains an API Token which authorizes this application to command the
        /// BPIM hardware through the API.
        /// 
        /// Upon a connection request, the hardware will initialize (this may take several seconds), and a token is returned.
        /// </summary>
        /// <param name="url">The host URL (default is localhost) or IP Address where the BPIM Web Engine is running</param>
        /// <param name="port">The port (usually port 80) on which the BPIM Web Engine is listening</param>
        /// <returns>Token string (HEX encoded), or error message</returns>
        private string apiConnect(string url, string port)
        {
            apiBaseUrl = "http://" + url + ":" + port + "/api";

            try
            {
                apiConnectToken = apiGetToken();
                apiSetPreset();
                apiSetModeSweepTx();
                return apiConnectToken;
            }
            catch (Exception exc)
            {
                apiDisconnect();
                return exc.Message;
            }
        }

        /// <summary>
        /// Commands the BPIM Web Engine to shutdown communications with the hardware.
        /// </summary>
        private void apiDisconnect()
        {
            try 
            { 
                client.DownloadString(apiCommand("setExit")); 
            } 
            catch (Exception e) { }

            apiBaseUrl = apiConnectToken = "";
        }

        /// <summary>
        /// Retrieve a token from a running BPIM Web Engine triggering an
        /// initialization of the BPIM hardware (if uninitialized).
        /// </summary>
        /// <returns>Token String (HEX encoded), or error message upon failure</returns>
        private string apiGetToken()
        {
            string response = "";

            try
            {
                response = client.DownloadString(apiBaseUrl + "/connect");
                System.Xml.XmlDocument xDoc = new XmlDocument();
                xDoc.InnerXml = response;
                apiConnectToken = response = xDoc.LastChild.InnerText;
            }
            catch (WebException e)
            {
                response = "A Web Exception Occurred. '" + e.Message + "'";
            }
            catch (NotSupportedException e)
            {
                response = "URL Format Not Supported. '" + e.Message + "'";
            }

            return response;
        }

        /// <summary>
        /// Preset the instrument in preperation for measurement.
        /// </summary>
        private void apiSetPreset()
        {
            client.DownloadString(apiCommand("setPreset"));
        }

        /// <summary>
        /// Set the analyzer for Swept Frequency Measurements. The boolean parameter
        /// passed in the URL tells the sweeper to perform a full-sweep then return 
        /// all of the data at once during my next getSamples call.
        /// </summary>
        private void apiSetModeSweepTx()
        {
            client.DownloadString(apiCommand("setModeSweepTx?Boolean=True"));
        }

        /// <summary>
        /// Trigger the measurement to occur.
        /// </summary>
        private void apiSetTrigger()
        {
            client.DownloadString(apiCommand("setTrigger"));
        }

        /// <summary>
        /// Retrieve the collection of samples measured as of the last trigger command.
        /// </summary>
        /// <returns></returns>
        private string apiGetSamples()
        {
            return client.DownloadString(apiCommand("getSamples"));
        }

        #endregion
    }
}
