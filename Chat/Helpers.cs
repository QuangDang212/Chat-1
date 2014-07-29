#region License Information (GPL v3)

/*
    Copyright (C) Jaex

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

namespace Chat
{
    public static class Helpers
    {
        public static string[] SplitMessage(string message, int number)
        {
            string[] messages = new string[number];

            if (message.Split().Length >= number)
            {
                for (int i = 0; i < number; i++)
                {
                    if (i == number - 1)
                    {
                        messages[i] = message;
                    }
                    else
                    {
                        messages[i] = message.Substring(0, message.IndexOf(' '));
                        message = message.Remove(0, messages[i].Length + 1);
                    }
                }
            }

            return messages;
        }
    }
}