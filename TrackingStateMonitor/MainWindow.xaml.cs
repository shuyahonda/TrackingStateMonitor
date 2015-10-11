using Microsoft.Kinect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrackingStateMonitor
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private KinectSensor kinect;
        private BodyFrameReader bodyFrameReader;
        private Body[] bodies;

        private Boolean isReading;

        private int allFrameCount;
        public int AllFrameCount
        {
            get
            {
                return this.allFrameCount;
            }
            set
            {
                allFrameCount = value;
                OnPropertyChanged("AllFrameCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        public MainWindow()
        {
            InitializeComponent();

            this.stopButton_Click(null, null);
            this.DataContext = this;
            AllFrameCount = 0;
            


            // Init Kinect
            kinect = KinectSensor.GetDefault();
            if (kinect == null)
            {
                throw new Exception("Kinectを開けませんでした。");
            }

            kinect.Open();
            this.bodies = new Body[kinect.BodyFrameSource.BodyCount];
            bodyFrameReader = kinect.BodyFrameSource.OpenReader();
            bodyFrameReader.FrameArrived += bodyFrameReader_FrameArrived;
        }

        private void bodyFrameReader_FrameArrived(object sender, BodyFrameArrivedEventArgs e)
        {
            if (this.isReading == false)
            {
                return;
            }

            this.AllFrameCount++;

            using (var bodyFrame = e.FrameReference.AcquireFrame())
            {
                if (bodyFrame == null)
                {
                    return;
                }
                // ボディデータを取得する

                bodyFrame.GetAndRefreshBodyData(bodies);

                //ボディがトラッキングできている
                foreach (var body in bodies.Where(b => b.IsTracked))
                {
                    // 1. Ankle Left
                    if (body.Joints[JointType.AnkleLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(AnkleLeftLabel,true, false, false);
                       
                    }
                    else if (body.Joints[JointType.AnkleLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(AnkleLeftLabel,false, true, false);
                        int value = int.Parse(AnkleLeftInferred.Content.ToString());
                        value++;
                        AnkleLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.AnkleLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(AnkleLeftLabel,false, false, true);
                        int value = int.Parse(AnkleLeftNotTracked.Content.ToString());
                        value++;
                        AnkleLeftNotTracked.Content = value.ToString();
                    }

                    // 2. Ankle Right
                    if (body.Joints[JointType.AnkleRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(AnkleRightLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.AnkleRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(AnkleRightLabel ,false, true, false);

                        int value = int.Parse(AnkleRightInferred.Content.ToString());
                        value++;
                        AnkleRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.AnkleRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(AnkleRightLabel,false, false, true);

                        int value = int.Parse(AnkleRightNotTracked.Content.ToString());
                        value++;
                        AnkleRightNotTracked.Content = value.ToString();
                    }

                    // 3. Elbow Left
                    if (body.Joints[JointType.ElbowLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ElbowLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.ElbowLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ElbowLeftLabel ,false, true, false);

                        int value = int.Parse(ElbowLeftInferred.Content.ToString());
                        value++;
                        ElbowLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ElbowLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ElbowLeftLabel ,false, false, true);

                        int value = int.Parse(ElbowLeftNotTracked.Content.ToString());
                        value++;
                        ElbowLeftNotTracked.Content = value.ToString();
                    }

                    // 4. Elbow Right
                    if (body.Joints[JointType.ElbowRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ElbowRightLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.ElbowRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ElbowRightLabel,false, true, false);

                        int value = int.Parse(ElbowRightInferred.Content.ToString());
                        value++;
                        ElbowRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ElbowRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ElbowRightLabel,false, false, true);

                        int value = int.Parse(ElbowRightInferred.Content.ToString());
                        value++;
                        ElbowRightNotTracked.Content = value.ToString();
                    }

                    // 5. Foot Left
                    if (body.Joints[JointType.FootLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(FootLeftLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.FootLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(FootLeftLabel,false, true, false);

                        int value = int.Parse(FootLeftInferred.Content.ToString());
                        value++;
                        FootLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.FootLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(FootLeftLabel,false, false, true);

                        int value = int.Parse(FootLeftNotTracked.Content.ToString());
                        value++;
                        FootLeftNotTracked.Content = value.ToString();
                    }

                    // 6. Foot Right
                    if (body.Joints[JointType.FootRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(FootRightLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.FootRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(FootRightLabel,false, true, false);
                        int value = int.Parse(FootRightInferred.Content.ToString());
                        value++;
                        FootRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.FootRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(FootRightLabel,false, false, true);
                        int value = int.Parse(FootRightNotTracked.Content.ToString());
                        value++;
                        FootRightNotTracked.Content = value.ToString();
                    }

                    // 7. Hand Left
                    if (body.Joints[JointType.HandLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HandLeftLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.HandLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HandLeftLabel ,false, true, false);
                        int value = int.Parse(HandLeftInferred.Content.ToString());
                        value++;
                        HandLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HandLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HandLeftLabel ,false, false, true);

                        int value = int.Parse(HandLeftNotTracked.Content.ToString());
                        value++;
                        HandLeftNotTracked.Content = value.ToString();
                    }

                    // 8. Hand Right
                    if (body.Joints[JointType.HandRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HandRightLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.HandRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HandRightLabel ,false, true, false);

                        int value = int.Parse(HandRightInferred.Content.ToString());
                        value++;
                        HandRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HandRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HandRightLabel ,false, false, true);

                        int value = int.Parse(HandRightNotTracked.Content.ToString());
                        value++;
                        HandRightNotTracked.Content = value.ToString();
                    }

                    // 9. HandTipLeft
                    if (body.Joints[JointType.HandTipLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HandTipLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.HandTipLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HandTipLeftLabel ,false, true, false);

                        int value = int.Parse(HandTipLeftInferred.Content.ToString());
                        value++;
                        HandTipLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HandTipLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HandTipLeftLabel ,false, false, true);

                        int value = int.Parse(HandTipLeftNotTracked.Content.ToString());
                        value++;
                        HandTipLeftNotTracked.Content = value.ToString();
                    }

                    // 10. HandTipRight
                    if (body.Joints[JointType.HandTipRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HandTipRightLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.HandTipRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HandTipRightLabel ,false, true, false);

                        int value = int.Parse(HandTipRightInferred.Content.ToString());
                        value++;
                        HandTipLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HandTipRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HandTipRightLabel ,false, false, true);

                        int value = int.Parse(HandTipRightNotTracked.Content.ToString());
                        value++;
                        HandTipRightNotTracked.Content = value.ToString();
                    }

                    // 11. Head
                    if (body.Joints[JointType.Head].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HeadLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.Head].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HeadLabel ,false, true, false);

                        int value = int.Parse(HeadInferred.Content.ToString());
                        value++;
                        HeadInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.Head].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HeadLabel ,false, false, true);

                        int value = int.Parse(HeadNotTracked.Content.ToString());
                        value++;
                        HeadNotTracked.Content = value.ToString();
                    }

                    // 12. Hip Left
                    if (body.Joints[JointType.HipLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HipLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.HipLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HipLeftLabel ,false, true, false);

                        int value = int.Parse(HipLeftInferred.Content.ToString());
                        value++;
                        HipLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HipLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HipLeftLabel ,false, false, true);

                        int value = int.Parse(HipLeftNotTracked.Content.ToString());
                        value++;
                        HipLeftNotTracked.Content = value.ToString();
                    }

                    // 13. Hip Right
                    if (body.Joints[JointType.HipRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(HipRightLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.HipRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(HipRightLabel ,false, true, false);

                        int value = int.Parse(HipRightInferred.Content.ToString());
                        value++;
                        HipRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.HipRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(HipRightLabel ,false, false, true);

                        int value = int.Parse(HipRightNotTracked.Content.ToString());
                        value++;
                        HipRightNotTracked.Content = value.ToString();
                    }

                    // 14. Knee Left
                    if (body.Joints[JointType.KneeLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(KneeLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.KneeLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(KneeLeftLabel ,false, true, false);

                        int value = int.Parse(KneeLeftInferred.Content.ToString());
                        value++;
                        KneeLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.KneeLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(KneeLeftLabel ,false, false, true);

                        int value = int.Parse(KneeLeftNotTracked.Content.ToString());
                        value++;
                        KneeLeftNotTracked.Content = value.ToString();
                    }

                    // 15. Knee Right
                    if (body.Joints[JointType.KneeRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(KneeRightLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.KneeRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(KneeRightLabel ,false, true, false);

                        int value = int.Parse(KneeRightInferred.Content.ToString());
                        value++;
                        KneeRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.KneeRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(KneeRightLabel ,false, false, true);

                        int value = int.Parse(KneeRightNotTracked.Content.ToString());
                        value++;
                        KneeRightNotTracked.Content = value.ToString();
                    }

                    // 16. Neck
                    if (body.Joints[JointType.Neck].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(NeckLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.Neck].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(NeckLabel ,false, true, false);

                        int value = int.Parse(NeckInferred.Content.ToString());
                        value++;
                        NeckInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.Neck].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(NeckLabel ,false, false, true);

                        int value = int.Parse(NeckNotTracked.Content.ToString());
                        value++;
                        NeckNotTracked.Content = value.ToString();
                    }

                    // 17. Shoulder Left
                    if (body.Joints[JointType.ShoulderLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ShoulderLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.ShoulderLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ShoulderLeftLabel,false, true, false);

                        int value = int.Parse(ShoulderLeftInferred.Content.ToString());
                        value++;
                        ShoulderLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ShoulderLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ShoulderLeftLabel ,false, false, true);

                        int value = int.Parse(ShoulderLeftNotTracked.Content.ToString());
                        value++;
                        ShoulderLeftNotTracked.Content = value.ToString();
                    }

                    // 18. Shoulder Right
                    if (body.Joints[JointType.ShoulderRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ShoulderRightLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.ShoulderRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ShoulderRightLabel,false, true, false);

                        int value = int.Parse(ShoulderRightInferred.Content.ToString());
                        value++;
                        ShoulderRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ShoulderRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ShoulderRightLabel,false, false, true);

                        int value = int.Parse(ShoulderRightNotTracked.Content.ToString());
                        value++;
                        ShoulderRightNotTracked.Content = value.ToString();
                    }

                    // 19. Spine Base
                    if (body.Joints[JointType.SpineBase].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(SpineBaseLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.SpineBase].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(SpineBaseLabel,false, true, false);

                        int value = int.Parse(SpineBaseInferred.Content.ToString());
                        value++;
                        SpineBaseInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.SpineBase].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(SpineBaseLabel,false, false, true);

                        int value = int.Parse(SpineBaseNotTracked.Content.ToString());
                        value++;
                        SpineBaseNotTracked.Content = value.ToString();
                    }

                    // 20. Spine Mid
                    if (body.Joints[JointType.SpineMid].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(SpineMidLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.SpineMid].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(SpineMidLabel,false, true, false);

                        int value = int.Parse(SpineMidInferred.Content.ToString());
                        value++;
                        SpineMidInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.SpineMid].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(SpineMidLabel ,false, false, true);

                        int value = int.Parse(SpineMidNotTracked.Content.ToString());
                        value++;
                        SpineMidNotTracked.Content = value.ToString();
                    }

                    // 21. Spine Shoulder
                    if (body.Joints[JointType.SpineShoulder].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(SpineShoulderLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.SpineShoulder].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(SpineShoulderLabel,false, true, false);

                        int value = int.Parse(SpineShoulderInferred.Content.ToString());
                        value++;
                        SpineShoulderInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.SpineShoulder].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(SpineShoulderLabel,false, false, true);

                        int value = int.Parse(SpineShoulderNotTracked.Content.ToString());
                        value++;
                        SpineShoulderNotTracked.Content = value.ToString();
                    }

                    // 22. ThumbLeft
                    if (body.Joints[JointType.ThumbLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ThumbLeftLabel ,true, false, false);
                    }
                    else if (body.Joints[JointType.ThumbLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ThumbLeftLabel,false, true, false);

                        int value = int.Parse(ThumbLeftInferred.Content.ToString());
                        value++;
                        ThumbLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ThumbLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ThumbLeftLabel,false, false, true);

                        int value = int.Parse(ThumbLeftNotTracked.Content.ToString());
                        value++;
                        ThumbLeftNotTracked.Content = value.ToString();
                    }

                    // 23. ThumbRight
                    if (body.Joints[JointType.ThumbRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(ThumbRightLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.ThumbRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(ThumbRightLabel,false, true, false);

                        int value = int.Parse(ThumbRightInferred.Content.ToString());
                        value++;
                        ThumbRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.ThumbRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(ThumbRightLabel,false, false, true);

                        int value = int.Parse(ThumbRightNotTracked.Content.ToString());
                        value++;
                        ThumbRightNotTracked.Content = value.ToString();
                    }

                    // 24. WristLeft
                    if (body.Joints[JointType.WristLeft].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(WristLeftLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.WristLeft].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(WristLeftLabel,false, true, false);

                        int value = int.Parse(WristLeftInferred.Content.ToString());
                        value++;
                        WristLeftInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.WristLeft].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(WristLeftLabel,false, false, true);

                        int value = int.Parse(WristLeftNotTracked.Content.ToString());
                        value++;
                        WristLeftNotTracked.Content = value.ToString();
                    }

                    // 25. WristRight
                    if (body.Joints[JointType.WristRight].TrackingState == TrackingState.Tracked)
                    {
                        setTrackingStateLabelColor(WristRightLabel,true, false, false);
                    }
                    else if (body.Joints[JointType.WristRight].TrackingState == TrackingState.Inferred)
                    {
                        setTrackingStateLabelColor(WristRightLabel,false, true, false);

                        int value = int.Parse(WristRightInferred.Content.ToString());
                        value++;
                        WristRightInferred.Content = value.ToString();
                    }
                    else if (body.Joints[JointType.WristRight].TrackingState == TrackingState.NotTracked)
                    {
                        setTrackingStateLabelColor(WristRightLabel,false, false, true);

                        int value = int.Parse(WristRightNotTracked.Content.ToString());
                        value++;
                        WristRightNotTracked.Content = value.ToString();
                    }

                  
                }
            }
        }

        private void setTrackingStateLabelColor(Label label,Boolean tracked, Boolean inferred, Boolean notTracked)
        {
            if (tracked == true)
            {
                label.Background = Brushes.Green;
            }
            else if (inferred == true)
            {
                label.Background = Brushes.Blue;
            }
            else if (notTracked == true)
            {
                label.Background = Brushes.Red;
            }
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            this.isReading = true;
            readStateLabel.Text = "取得中";
            readStateLabel.Background = Brushes.Blue;
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            this.isReading = false;
            readStateLabel.Text = "停止中";
            readStateLabel.Background = Brushes.OrangeRed;
        }

        private void resetButton_Click(object sender, RoutedEventArgs e)
        {
            AllFrameCount = 0;
            AnkleLeftInferred.Content = "0";
            AnkleLeftNotTracked.Content = "0";
            AnkleRightInferred.Content = "0";
            AnkleRightNotTracked.Content = "0";
            ElbowLeftInferred.Content = "0";
            ElbowLeftNotTracked.Content = "0";
            ElbowRightInferred.Content = "0";
            ElbowRightNotTracked.Content = "0";
            FootLeftInferred.Content = "0";
            FootLeftNotTracked.Content = "0";
            FootRightInferred.Content = "0";
            FootRightNotTracked.Content = "0";
            HandLeftInferred.Content = "0";
            HandLeftNotTracked.Content = "0";
            HandRightInferred.Content = "0";
            HandRightNotTracked.Content = "0";
            HandTipLeftInferred.Content = "0";
            HandTipLeftNotTracked.Content = "0";
            HandTipRightInferred.Content = "0";
            HandTipRightNotTracked.Content = "0";
            HeadInferred.Content = "0";
            HeadNotTracked.Content = "0";
            HipLeftInferred.Content = "0";
            HipLeftNotTracked.Content = "0";
            HipRightInferred.Content = "0";
            HipRightNotTracked.Content = "0";
            KneeLeftInferred.Content = "0";
            KneeLeftNotTracked.Content = "0";
            KneeRightInferred.Content = "0";
            KneeRightNotTracked.Content = "0";
            NeckInferred.Content = "0";
            NeckNotTracked.Content = "0";
            ShoulderLeftInferred.Content = "0";
            ShoulderLeftNotTracked.Content = "0";
            ShoulderRightInferred.Content = "0";
            ShoulderRightNotTracked.Content = "0";
            SpineBaseInferred.Content = "0";
            SpineBaseNotTracked.Content = "0";
            SpineMidInferred.Content = "0";
            SpineMidNotTracked.Content = "0";
            SpineShoulderInferred.Content = "0";
            SpineShoulderNotTracked.Content = "0";
            ThumbLeftInferred.Content = "0";
            ThumbLeftNotTracked.Content = "0";
            ThumbRightInferred.Content = "0";
            ThumbRightNotTracked.Content = "0";
            WristRightInferred.Content = "0";
            WristRightNotTracked.Content = "0";
        }
    }
}
