using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using MCP;

namespace Sorting.Dispatching.Util
{
    public class LEDUtil2015
    {
        private LedCollection leds = new LedCollection();
        public Dictionary<int, int> errChannelAddress = new Dictionary<int, int>();

        public LEDUtil2015()
        {
            //³õÊ¼»¯LEDÆÁ            
            leds.DelAllProgram();
        }

        public void Show(DataRow[] channelRows, bool checkMode)
        {
            foreach (DataRow row in channelRows)
            {


                string text = "";
                string channelName = row["CHANNELNAME"].ToString().Trim().Substring(1, 2);
                string cigaretteName = row["CIGARETTENAME"].ToString().Trim();
                string quantity = StringUtil.ToSBC(Convert.ToString((Convert.ToInt32(row["REMAINQUANTITY"]) % 50)).PadRight(2, " "[0]));
                int channelAddress = Convert.ToInt32(row["CHANNELADDRESS"]);

                cigaretteName = cigaretteName.Replace("(", StringUtil.ToSBC(""));
                cigaretteName = cigaretteName.Replace(")", StringUtil.ToSBC(""));
                cigaretteName = cigaretteName.Replace("£¨", StringUtil.ToSBC(""));
                cigaretteName = cigaretteName.Replace("£©", StringUtil.ToSBC(""));
                cigaretteName = cigaretteName.Replace(" ", "");
                cigaretteName = cigaretteName.Replace("  ", "");

                int led_card = Convert.ToInt32(row["LEDNO"]);

                int led_x = Convert.ToInt32(row["LED_X"]);
                int led_y = Convert.ToInt32(row["LED_Y"]);
                int led_width = Convert.ToInt32(row["LED_WIDTH"]);
                int led_height = Convert.ToInt32(row["LED_HEIGHT"]);

                if (cigaretteName.Length == 0)
                {
                    text = string.Format("{0} {1} {2}", "", "  ÎÞ  ", "");
                    leds.AddTextToProgram(led_card, led_x, led_y, led_height, led_width, text, Util.LED2008.RED, false);
                }
                else
                {
                    if (checkMode && Convert.ToInt32((Convert.ToInt32(row["REMAINQUANTITY"]) % 50)) >= 0)
                    {
                        text = string.Format("{0}{1}", quantity, cigaretteName);
                        if (errChannelAddress.ContainsKey(channelAddress))
                        {
                            leds.AddTextToProgram(led_card, led_x, led_y, led_height, led_width, text, Util.LED2008.GREEN, true);
                        }
                        else
                        {
                            leds.AddTextToProgram(led_card, led_x, led_y, led_height, led_width, text, Util.LED2008.RED, true);
                        }
                    }
                    else
                    {
                        text = string.Format("{0}{1}", "", cigaretteName);

                        if (errChannelAddress.ContainsKey(channelAddress))
                        {
                            leds.AddTextToProgram(led_card, led_x, led_y, led_height, led_width, text, Util.LED2008.GREEN, true);
                        }
                        else
                        {
                            leds.AddTextToProgram(led_card, led_x, led_y, led_height, led_width, text, Util.LED2008.RED, true);
                        }
                    }
                }
            }

        }
    }
}
