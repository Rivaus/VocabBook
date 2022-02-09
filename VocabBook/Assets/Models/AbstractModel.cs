/*
MIT License

Copyright (c) 2022 Quentin Tran

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections.Generic;
using System.Linq;

namespace com.quentintran.models
{
    [System.Serializable]
    public abstract class AbstractModel 
    {
        public static Dictionary<System.Type, HashSet<AbstractModel>> entities = new Dictionary<System.Type, HashSet<AbstractModel>>();

        public int id;
        public int Id { get => id; private set => id = value; }

        /// <summary>
        /// Creates a new model with a unique id for its type.
        /// </summary>
        public AbstractModel()
        {
            System.Type type = GetType();

            if (entities.ContainsKey(type))
            {
                var set = entities[type];
                int id = set.Count;
                while (set.Count(e => e.Id == id) > 0)
                    id++;

                Id = id;
                set.Add(this);
            } else
            {
                entities.Add(type, new HashSet<AbstractModel> { this });
                this.Id = 0;
            }
        }

        public void Delete()
        {
            System.Type type = GetType();

            if (entities.ContainsKey(type))
            {
                entities[type].Remove(this);
            }
        }
    }
}