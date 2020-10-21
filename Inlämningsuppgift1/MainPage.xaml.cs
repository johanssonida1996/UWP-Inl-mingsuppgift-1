using Inlämningsuppgift1.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Inlämningsuppgift1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
       
        public MainPage()
        {
            this.InitializeComponent();            
                        
        }

        public ContentList contentList = new ContentList();     



        private async void openFileExplorerBtn_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.List;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            picker.FileTypeFilter.Add(".xml");
            picker.FileTypeFilter.Add(".csv");
            picker.FileTypeFilter.Add(".json");
            picker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await picker.PickSingleFileAsync();



            if (file != null)
            {
                
                if (file.ContentType == "application/vnd.ms-excel")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);

                   
                    try
                    {
                        contentList.Add(new Content($"Texten i filen är följande: {text}"));
                    }
                    catch { }
                }
               
                else if (file.ContentType == "text/xml")
                {
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(text);


                    foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
                    {
                        string textoutput = node.InnerText; //or loop through its children as well
                        try
                        {
                            contentList.Add(new Content($"{textoutput}"));
                        }
                        catch { }
                    }
                }

                else if (file.ContentType == "application/json")
                {
                    var path = file.Path;
                    
                    string text = await Windows.Storage.FileIO.ReadTextAsync(file);
                    var obj = JsonConvert.DeserializeObject<dynamic>(text);

                    try
                    {
                        contentList.Add(new Content($"Texten i filen är följande: {obj.message}"));
                    }
                    catch { }

                }
                }
                else
                {
                    this.textblock.Text = "Operation cancelled.";
                }


            }

        }
    }




