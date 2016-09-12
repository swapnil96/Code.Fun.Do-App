using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace App2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Game : Page
    {
        Board Game_Board = new Board();
        int prev_Click_Caller = 0;
        public bool pause = true;
        public Stopwatch stopwatch = new Stopwatch();
        public ApplicationDataContainer localsettings = ApplicationData.Current.LocalSettings;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Debug.WriteLine("Navigation first");
            string message = e.Parameter as String;
            if (message[0] == 'E')
            {
                GameConstant.min_vel = 150;
                GameConstant.max_vel = 400;
                GameConstant.min_accln = 50;
                GameConstant.max_accln = 200;
                GameConstant.diff_level = 0;
            }

            else if(message[0] == 'M')
            {
                GameConstant.min_vel = 250;
                GameConstant.max_vel = 500;
                GameConstant.min_accln = 100;
                GameConstant.max_accln = 250;
                GameConstant.diff_level = 1;
            }

            else if(message[0] == 'H')
            {
                GameConstant.min_vel = 400;
                GameConstant.max_vel = 800;
                GameConstant.min_accln = 200;
                GameConstant.max_accln = 300;
                GameConstant.diff_level = 2;
            }

            else
            {
                return;
            }
        }

        public Game()
        {
            this.InitializeComponent();
            Game_Board = new Board();
            Draw_Canvas();
            Game_Start();
        }

        public async void Game_Start()
        {
            stopwatch.Start();
            int i = 0;
            while (!pause && stopwatch.ElapsedMilliseconds <= GameConstant.time * 1000 + 1000)
            {
                Timer_Box.Text = (Convert.ToInt32(GameConstant.time - (stopwatch.ElapsedMilliseconds) / 1000)).ToString();
                Score_Box.Text = "Score - \n" + Game_Board.score;
                Game_Canvas.Children.Clear();
                if (i % 47 == 0) { GameConstant.min_vel += 1; GameConstant.max_vel += 1; }
                foreach (var obj in Game_Board.queue_Falling_obj)
                {
                    Image temp = new Image();
                    temp.Margin = new Thickness(obj.x_coord * GameConstant.scalingf_x, obj.y_coord * GameConstant.scalingf_y, 0, 0);
                    switch (obj.type)
                    {
                        case 0:
                            temp.Source = yellow.Source;
                            break;
                        case 1:
                            temp.Source = blue.Source;
                            break;
                        case 2:
                            temp.Source = orange.Source;
                            break;
                        case 3:
                            temp.Source = green.Source;
                            break;
                        case 4:
                            temp.Source = purple.Source;
                            break;
                        case 5:
                            temp.Source = red.Source;
                            break;
                        default:
                            Debug.WriteLine("BUG - color id - {0}", obj.type);
                            break;
                    }
                    Game_Canvas.Children.Add(temp);
                }
                Game_Board.Update_Pos_Falling_Obj();
                //int temp_pos = Game_Board.queue_Falling_obj.Peek().pos;
                /*if (Game_Board.Collision_Detect())
                {
                    switch (temp_pos)
                    {
                        case 0:
                            textBlock_1.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        case 1:
                            textBlock_2.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        case 2:
                            textBlock_3.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        case 3:
                            textBlock_4.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        case 4:
                            textBlock_5.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        case 5:
                            textBlock_6.Text = Game_Board.arr_Stat_obj[temp_pos].mass.ToString();
                            break;
                        default:
                            Debug.WriteLine("EXCEPTION - pos - {0}", temp_pos);
                            break;
                    }
                }*/
                Game_Board.Collision_Detect();
                await Task.Delay(1);
            }
            stopwatch.Stop();
            Game_Finish();
        }

        private void Container_Click(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Image Img_cal = sender as Image;
            int current_num = Convert.ToInt32(Img_cal.Tag);
            if (!stopwatch.IsRunning) { return; }

            if (prev_Click_Caller == 0)
            {
                prev_Click_Caller = current_num;
                Image_Border.Margin = new Thickness(Game_Board.arr_Stat_obj[prev_Click_Caller - 1].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[prev_Click_Caller - 1].y_coord * GameConstant.scalingf_y, GameConstant.dimensionx - (Game_Board.arr_Stat_obj[prev_Click_Caller - 1].x_coord + 150) * GameConstant.scalingf_x, GameConstant.dimensiony - (Game_Board.arr_Stat_obj[prev_Click_Caller - 1].y_coord + 150) * GameConstant.scalingf_y);
                Image_Border.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                Image_Border.Visibility = Visibility.Collapsed;
                if (prev_Click_Caller == current_num)
                {
                    prev_Click_Caller = 0;
                    return;
                }
                else
                {
                    Game_Board.Swap_Static_Obj(prev_Click_Caller - 1, current_num - 1);
                    switch (prev_Click_Caller)
                    {
                        case 1:
                            //textBlock_1.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_1.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        case 2:
                            //textBlock_2.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_2.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        case 3:
                            //textBlock_3.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_3.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        case 4:
                            //textBlock_4.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_4.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        case 5:
                            //textBlock_5.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_5.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        case 6:
                            //textBlock_6.Text = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].mass.ToString();
                            cont_6.Source = Game_Board.arr_Stat_obj[prev_Click_Caller - 1].object_img.Source;
                            break;
                        default:
                            Debug.WriteLine("EXCEPTION - pos - {0}", prev_Click_Caller - 1);
                            break;
                    }
                    switch (current_num)
                    {
                        case 1:
                            //textBlock_1.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_1.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        case 2:
                            //textBlock_2.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_2.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        case 3:
                            //textBlock_3.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_3.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        case 4:
                            //textBlock_4.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_4.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        case 5:
                            //textBlock_5.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_5.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        case 6:
                            //textBlock_6.Text = Game_Board.arr_Stat_obj[current_num - 1].mass.ToString();
                            cont_6.Source = Game_Board.arr_Stat_obj[current_num - 1].object_img.Source;
                            break;
                        default:
                            Debug.WriteLine("EXCEPTION - pos - {0}", current_num - 1);
                            break;
                    }
                    prev_Click_Caller = 0;
                }
            }
        }

        private void Pause_Function(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            if (pause)
            {
                pause = false;
                Pause_Button.Source = pause_img.Source;
                Game_Start();
                
            }
            else
            {
                pause = true;
                Pause_Button.Source = start_img.Source;
            }
        }

        private void Restart_Function(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            //Dont know why the below restart func speed up the game!
            //Game_Board = new Board();
            //stopwatch.Reset();
            //Game_Start();
            if (GameConstant.diff_level == 0) { Frame.Navigate(typeof(Game), "E"); }
            else if(GameConstant.diff_level == 1) { Frame.Navigate(typeof(Game), "M"); }
            else if (GameConstant.diff_level == 2) { Frame.Navigate(typeof(Game), "H"); }

        }

        public void Draw_Canvas()
        {
            Menu_Restart.Visibility = Visibility.Collapsed;
            Menu_Restart.Visibility = Visibility.Collapsed;
            Menu_Exit.Visibility = Visibility.Collapsed;
            Score_Final_Box.Visibility = Visibility.Collapsed;
            HighScore_Box.Visibility = Visibility.Collapsed;
            Pause_window.Visibility = Visibility.Collapsed;
            HS_Box.Visibility = Visibility.Collapsed;

            cont_1.Margin = new Thickness(Game_Board.arr_Stat_obj[0].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[0].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_1.Margin = new Thickness((GameConstant.pos_array[0] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[0] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_1.Text = "0";
            textBlock_1.FontSize = (22 * GameConstant.scalingf_x);
            cont_1.Visibility = Visibility.Visible;
            //textBlock_1.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[0].object_img.Source = cont_1.Source;

            cont_2.Margin = new Thickness(Game_Board.arr_Stat_obj[1].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[1].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_2.Margin = new Thickness((GameConstant.pos_array[1] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[1] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_2.Text = "0";
            textBlock_2.FontSize = (22 * GameConstant.scalingf_x);
            cont_2.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[1].object_img.Source = cont_2.Source;
            //textBlock_2.Visibility = Visibility.Visible;

            cont_3.Margin = new Thickness(Game_Board.arr_Stat_obj[2].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[5].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_3.Margin = new Thickness((GameConstant.pos_array[2] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[2] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_3.Text = "0";
            textBlock_3.FontSize = (22 * GameConstant.scalingf_x);
            cont_3.Visibility = Visibility.Visible;
            //textBlock_3.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[2].object_img.Source = cont_3.Source;

            cont_4.Margin = new Thickness(Game_Board.arr_Stat_obj[3].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[5].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_4.Margin = new Thickness((GameConstant.pos_array[3] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[3] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_4.Text = "0";
            textBlock_4.FontSize = (22 * GameConstant.scalingf_x);
            cont_4.Visibility = Visibility.Visible;
            //textBlock_4.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[3].object_img.Source = cont_4.Source;

            cont_5.Margin = new Thickness(Game_Board.arr_Stat_obj[4].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[5].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_5.Margin = new Thickness((GameConstant.pos_array[4] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[4] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_5.FontSize = (22 * GameConstant.scalingf_x);
            textBlock_5.Text = "0";
            cont_5.Visibility = Visibility.Visible;
            //textBlock_5.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[4].object_img.Source = cont_5.Source;

            cont_6.Margin = new Thickness(Game_Board.arr_Stat_obj[5].x_coord * GameConstant.scalingf_x, Game_Board.arr_Stat_obj[5].y_coord * GameConstant.scalingf_y, 0, 0);
            textBlock_6.Margin = new Thickness((GameConstant.pos_array[5] - 18) * GameConstant.scalingf_x, 684 * GameConstant.scalingf_y, (1366 - GameConstant.pos_array[5] - 18) * GameConstant.scalingf_x, 48 * GameConstant.scalingf_y);
            textBlock_6.Text = "0";
            textBlock_6.FontSize = (22 * GameConstant.scalingf_x);
            cont_6.Visibility = Visibility.Visible;
            //textBlock_6.Visibility = Visibility.Visible;
            Game_Board.arr_Stat_obj[5].object_img.Source = cont_6.Source;
        }

        private void Exit_Func(object sender, PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        public void Game_Finish()
        {
            if (stopwatch.ElapsedMilliseconds >= GameConstant.time * 1000)
            {
                stopwatch.Reset();
                Pause_window.Visibility = Visibility.Visible;
                Menu_Restart.Visibility = Visibility.Visible;
                Menu_Restart.Visibility = Visibility.Visible;
                Menu_Exit.Visibility = Visibility.Visible;
                Score_Final_Box.Visibility = Visibility.Visible;
                HS_Box.Visibility = Visibility.Visible;
                HighScore_Box.Visibility = Visibility.Visible;
                Score_Final_Box.Text = "Your Score - " + Game_Board.score.ToString();
                if(Convert.ToDouble(localsettings.Values[GameConstant.diff_level.ToString()]) < Game_Board.score)
                {
                    localsettings.Values[GameConstant.diff_level.ToString()] = Game_Board.score.ToString();
                }
                HS_Box.Text=("High Score - "+localsettings.Values[GameConstant.diff_level.ToString()].ToString());
                if (GameConstant.diff_level == 0)
                {
                    if (Game_Board.score < 30) { HighScore_Box.Text = "Good Try! But practise is required"; }
                    else { HighScore_Box.Text = "Welcome to the Ivy League!"; }
                }
                else if (GameConstant.diff_level == 1)
                {
                    if (Game_Board.score < 30) { HighScore_Box.Text = "Failures are stepping stones to success"; }
                    else { HighScore_Box.Text = "On the road to being a Jedi"; }
                }
                else
                {
                    if (Game_Board.score < 30) { HighScore_Box.Text = "Jedi doesn't Lose!"; }
                    else { HighScore_Box.Text = "With Great Power comes Great Responsibility"; }
                }
            }
        }

          }
}
