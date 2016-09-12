using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Tlank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace App2
{
    class Falling_Object
    {
        public double y_coord;
        public double x_coord;
        public int pos;
        public double vel_y;
        public double accln_y;
        public int type;
        public double width = 70;
        public double height = 70;
        public double mass = 1;

        public Falling_Object()
        {
            y_coord = 0;
            x_coord = 0;
            pos = 0;
            vel_y = 0;
            accln_y = 0;
            type = 0;
            height = 70;
            width = 70;
            mass = 1;
        }

        public Falling_Object(int pos, double vel, double accln_y, int color)
        {
            this.pos = pos;
            vel_y = vel;
            this.accln_y = accln_y;
            y_coord = 0;
            type = color;
            mass = 1;
            width = 70;
            height = 70;
            x_coord = (GameConstant.pos_array[pos] - width / 2);
        }

    }

    class Stat_Object
    {
        public double y_coord;
        public double x_coord;
        public int pos;
        public int type;
        public double height = 100;
        public double width = 150;
        public double mass = 0;
        public Image object_img = new Image();

        public Stat_Object()
        {
            y_coord = 600;
            x_coord = 0;
            pos = 0;
            type = 0;
            height = 150;
            width = 150;
            mass = 0;
        }

        public Stat_Object(int pos, int color)
        {
            y_coord = 600;
            this.pos = pos;
            type = color;
            mass = 0;
            x_coord = (GameConstant.pos_array[pos] - width / 2);
        }
    }

    public class GameConstant
    {
        public static double dimensionx = Window.Current.Bounds.Width;
        public static double dimensiony = Window.Current.Bounds.Height;
        public static int[] pos_array = { 95, 297, 499, 701, 903, 1105 };
        public static double scalingf_x = dimensionx / 1366;
        public static double scalingf_y = dimensiony / 768;
        public static int time = 60;
        public static int min_vel = 50;
        public static int min_accln = 15;
        public static int max_vel = 100;
        public static int max_accln = 200;
        public static int diff_level;

    }

    class Board
    {
        public Queue<Falling_Object> queue_Falling_obj;
        public List<Stat_Object> arr_Stat_obj;
        public double score = 0;
        public static Random rndgen = new Random();
        public int difficulty;
        public Board()
        {
            int i;
            queue_Falling_obj = new Queue<Falling_Object>();
            arr_Stat_obj = new List<Stat_Object>();
            for (i = 0; i < 6; i++)
            {
                Stat_Object temp = new Stat_Object(i, i);
                arr_Stat_obj.Add(temp);
            }
            queue_Falling_obj.Enqueue(new Falling_Object(3, 0.25, 0.1, 1));
            score = 0;
            difficulty = GameConstant.diff_level;
        }

        public bool Collision_Detect()
        {
            Falling_Object ball = queue_Falling_obj.Peek();
            Stat_Object box = arr_Stat_obj[ball.pos];
            if ((box.y_coord) > (ball.y_coord + (ball.height)))
            {
                if (box.y_coord <= ball.y_coord + ball.height + 10 * ball.vel_y +56*ball.accln_y && box.y_coord > ball.y_coord + ball.height + 9 * ball.vel_y + 46 * ball.accln_y)
                {
                    this.Add_Rand_Falling_Obj();
                }
                return false;
            }
            else
            {
                queue_Falling_obj.Dequeue();
                if (box.type == ball.type)
                {
                    score += 1;
                }
                else
                {
                    //score -= (arr_Stat_obj[ball.pos].mass + ball.mass);
                    //arr_Stat_obj[ball.pos].mass += ball.mass;
                }

                return true;
            }
        }

        public void Add_Rand_Falling_Obj()
        {
            /// <summary>
            /// Bug - Always same random falling Obj!!
            /// </summary>

            queue_Falling_obj.Enqueue(new Falling_Object(rndgen.Next(0, 500) % 6, rndgen.Next(GameConstant.min_vel, GameConstant.max_vel) / 800.0, rndgen.Next(GameConstant.min_accln, GameConstant.max_accln) / 1800.0, rndgen.Next(rndgen.Next(0, 727) % 6)));
        }

        public void Update_Pos_Falling_Obj()
        {
            foreach (var fall_obj in queue_Falling_obj.ToList())
            {
                fall_obj.vel_y += fall_obj.accln_y;
                fall_obj.y_coord += fall_obj.vel_y;
            }
        }

        public void Swap_Static_Obj(int pos1, int pos2)
        {
            double mass = arr_Stat_obj[pos1].mass;
            int type = arr_Stat_obj[pos1].type;
            Image temp = new Image();
            temp.Source = arr_Stat_obj[pos1].object_img.Source;

            arr_Stat_obj[pos1].mass = arr_Stat_obj[pos2].mass;
            arr_Stat_obj[pos1].type = arr_Stat_obj[pos2].type;
            arr_Stat_obj[pos1].object_img.Source = arr_Stat_obj[pos2].object_img.Source;

            arr_Stat_obj[pos2].mass = mass;
            arr_Stat_obj[pos2].type = type;
            arr_Stat_obj[pos2].object_img.Source = temp.Source;
            score -= arr_Stat_obj[pos1].mass + arr_Stat_obj[pos2].mass;
        }

    }













    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
#if WINDOWS_PHONE_APP
        private TransitionCollection transitions;
#endif

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += this.OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used when the application is launched to open a specific file, to display
        /// search results, and so forth.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                // TODO: change this value to a cache size that is appropriate for your application
                rootFrame.CacheSize = 1;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
#if WINDOWS_PHONE_APP
                // Removes the turnstile navigation for startup.
                if (rootFrame.ContentTransitions != null)
                {
                    this.transitions = new TransitionCollection();
                    foreach (var c in rootFrame.ContentTransitions)
                    {
                        this.transitions.Add(c);
                    }
                }

                rootFrame.ContentTransitions = null;
                rootFrame.Navigated += this.RootFrame_FirstNavigated;
#endif

                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                if (!rootFrame.Navigate(typeof(MainPage), e.Arguments))
                {
                    throw new Exception("Failed to create initial page");
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

#if WINDOWS_PHONE_APP
        /// <summary>
        /// Restores the content transitions after the app has launched.
        /// </summary>
        /// <param name="sender">The object where the handler is attached.</param>
        /// <param name="e">Details about the navigation event.</param>
        private void RootFrame_FirstNavigated(object sender, NavigationEventArgs e)
        {
            var rootFrame = sender as Frame;
            rootFrame.ContentTransitions = this.transitions ?? new TransitionCollection() { new NavigationThemeTransition() };
            rootFrame.Navigated -= this.RootFrame_FirstNavigated;
        }
#endif

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}