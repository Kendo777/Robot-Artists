using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenMarcLozano
{

    class Robot
    {
        private int r = 0;
        private int g = 0;
        private int b = 0;
        private char name;
        private int[] buckets = new int[3];
        ConsoleColor consoleColor;
        public Robot(char name, int r, int g, int b, ConsoleColor consoleColor)
        {
            this.name = name;
            this.r = r;
            this.g = g;
            this.b = b;
            this.consoleColor = consoleColor;
            for(int i=0; i<3;i++)
            {
                buckets[i] = 0;
            }
        }
        public int getRed()
        {
            return r;
        }
        public int getGreen()
        {
            return g;
        }
        public int getBlue()
        {
            return b;
        }
        public char getName()
        {
            return name;
        }
        public ConsoleColor getColor()
        {
            return consoleColor;
        }
        public void getBuckets()
        {
            buckets[0] = r;
            buckets[1] = g;
            buckets[2] = b;
        }
        public void useBuckets()
        {
            for (int i = 0; i < 3; i++)
            {
                buckets[i] = 0;
            }
        }
        public bool emptyBuckets()
        {
            if(buckets[0]<r)
            {
                return true;
            }
            if(buckets[1] < g)
            {
                return true;
            }
            if (buckets[2] < b)
            {
                return true;
            }
            return false;
        }
    }
}
