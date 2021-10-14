using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotController
{

    public struct MyQuat
    {

        public float w;
        public float x;
        public float y;
        public float z;

        public MyQuat(float _x, float _y, float _z, float _w)
        {
            x = _x;
            y = _y;
            z = _z;
            w = _w;
        }
    }

    public struct MyVec
    {

        public float x;
        public float y;
        public float z;

        public MyVec(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }






    public class MyRobotController
    {

        #region public methods



        public string Hi()
        {

            string s = "Juega a Soccer Legends";
            return s;

        }


        //EX1: this function will place the robot in the initial position

        public void PutRobotStraight(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            finished = false;
            //todo: change this, use the function Rotate declared below
            rot0 = Rotate(new MyQuat(0, 0, 0, 1), new MyVec(0, 1, 0), exercise1Positions[0]);
            rot1 = Multiply(rot0, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), exercise1Positions[1]));
            rot2 = Multiply(rot1, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), exercise1Positions[2]));
            rot3 = Multiply(rot2, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), exercise1Positions[3]));
        }



        //EX2: this function will interpolate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.


        public bool PickStudAnim(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            //todo: add a check for your condition
            if (!myCondition && !finished)
            {
                lerpValue = 0;
                myCondition = true;
            }

            if (myCondition)
            {
                //todo: add your code here
                lerpValue += 0.01f;

                float angle0 = LerpRotations(0, exercise2Positions[0], lerpValue);
                float angle1 = LerpRotations(exercise1Positions[1], exercise2Positions[1], lerpValue);
                float angle2 = LerpRotations(exercise1Positions[2], exercise2Positions[2], lerpValue);
                float angle3 = LerpRotations(exercise1Positions[3], exercise2Positions[3], lerpValue);

                if (angle0 <= exercise2Positions[0] && angle1 >= exercise2Positions[1] && angle2 >= exercise2Positions[2] && angle3 <= exercise2Positions[3])
                {
                    myCondition = false;
                    finished = true;
                }

                MyQuat aux0 = Rotate(new MyQuat(0, 0, 0, 1), new MyVec(0, 1, 0), exercise1Positions[0]);

                rot0 = Rotate(aux0, new MyVec(0, 1, 0), angle0);
                rot1 = Multiply(rot0, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle1));
                rot2 = Multiply(rot1, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle2));
                rot3 = Multiply(rot2, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle3));
                return true;
            }
            else
            {
                //todo: remove this once your code works.
                rot0 = NullQ;
                rot1 = NullQ;
                rot2 = NullQ;
                rot3 = NullQ;

                return false;
            }

        }


        //EX3: this function will calculate the rotations necessary to move the arm of the robot until its end effector collides with the target (called Stud_target)
        //it will return true until it has reached its destination. The main project is set up in such a way that when the function returns false, the object will be droped and fall following gravity.
        //the only difference wtih exercise 2 is that rot3 has a swing and a twist, where the swing will apply to joint3 and the twist to joint4

        public bool PickStudAnimVertical(out MyQuat rot0, out MyQuat rot1, out MyQuat rot2, out MyQuat rot3)
        {

            bool myCondition = true;
            //todo: add a check for your condition

            float lerpValue = 0;

            if (myCondition)
            {
                //todo: add your code here
                lerpValue += 0.01f;

                float angle0 = LerpRotations(exercise1Positions[0], exercise2Positions[0], lerpValue);
                float angle1 = LerpRotations(exercise1Positions[1], exercise2Positions[1], lerpValue);
                float angle2 = LerpRotations(exercise1Positions[2], exercise2Positions[2], lerpValue);
                float angle3 = LerpRotations(exercise1Positions[3], exercise2Positions[3], lerpValue);

                if (angle0 >= exercise2Positions[0] && angle1 >= exercise2Positions[1] && angle2 >= exercise2Positions[2] && angle3 >= exercise2Positions[3])
                {
                    myCondition = false;
                }
                rot0 = Rotate(new MyQuat(0, 0, 0, 1), new MyVec(0, 1, 0), angle0);
                rot1 = Multiply(rot0, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle1));
                rot2 = Multiply(rot1, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle2));
                rot3 = Multiply(rot2, Rotate(new MyQuat(0, 0, 0, 1), new MyVec(1, 0, 0), angle3));
                return true;
            }

            else//todo: remove this once your code works.
                rot0 = NullQ;
            rot1 = NullQ;
            rot2 = NullQ;
            rot3 = NullQ;

            return false;
        }


        public static MyQuat GetSwing(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }


        public static MyQuat GetTwist(MyQuat rot3)
        {
            //todo: change the return value for exercise 3
            return NullQ;

        }




        #endregion


        #region private and internal methods

        float[] exercise1Positions = { 73.434f, -7.25f, 69.372f, 53.615f };
        float[] exercise2Positions = { -36.495f, 0.0f, 76.768f, 22.703f };
        float lerpValue = 0;
        bool myCondition = false;
        bool finished = false;

        internal int TimeSinceMidnight { get { return (DateTime.Now.Hour * 3600000) + (DateTime.Now.Minute * 60000) + (DateTime.Now.Second * 1000) + DateTime.Now.Millisecond; } }


        private static MyQuat NullQ
        {
            get
            {
                MyQuat a;
                a.w = 1;
                a.x = 0;
                a.y = 0;
                a.z = 0;
                return a;

            }
        }

        internal MyQuat Multiply(MyQuat _q1, MyQuat _q2)
        {

            //todo: change this so it returns a multiplication:
            MyQuat q = new MyQuat();

            q.x = (_q1.w * _q2.x) + (_q1.x * _q2.w) + (_q1.y * _q2.z) - (_q1.z * _q2.y);
            q.y = (_q1.w * _q2.y) - (_q1.x * _q2.z) + (_q1.y * _q2.w) + (_q1.z * _q2.x);
            q.z = (_q1.w * _q2.z) + (_q1.x * _q2.y) - (_q1.y * _q2.x) + (_q1.z * _q2.w);
            q.w = (_q1.w * _q2.w) - (_q1.x * _q2.x) - (_q1.y * _q2.y) - (_q1.z * _q2.z);
            return q;

        }

        internal MyQuat Rotate(MyQuat _rot, MyVec _axis, float _angle)
        {


            MyQuat resultQuatAxisAngle = new MyQuat();

            resultQuatAxisAngle.x = (float)(_axis.x * Math.Sin(_angle * Math.PI / 360));
            resultQuatAxisAngle.y = (float)(_axis.y * Math.Sin(_angle * Math.PI / 360));
            resultQuatAxisAngle.z = (float)(_axis.z * Math.Sin(_angle * Math.PI / 360));
            resultQuatAxisAngle.w = (float)Math.Cos((_angle * Math.PI) / 360);

            Normalize(resultQuatAxisAngle);

            return Multiply(resultQuatAxisAngle, _rot);
        }




        //todo: add here all the functions needed
        public void Normalize(MyQuat _q)
        {
            double m = Math.Sqrt(Math.Pow(_q.x, 2) + Math.Pow(_q.y, 2) + Math.Pow(_q.z, 2) + Math.Pow(_q.w, 2));
            _q.x /= (float)m;
            _q.y /= (float)m;
            _q.z /= (float)m;
            _q.w /= (float)m;
        }

        #endregion

        public float LerpRotations(float _start, float _end, float _lerpValue)
        {
            return _start + _lerpValue * (_end - _start);
        }




    }
}
