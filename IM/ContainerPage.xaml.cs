using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=234238에 나와 있습니다.

namespace IM
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class ContainerPage : Page
    {
        public int idx = 0;

        public ContainerPage()
        {
            this.InitializeComponent();
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            // 컨테이너 공간 생성
            StackPanel ctSpace = new StackPanel()
            {
                Name = "ctSpace" + (++idx).ToString(),
                Orientation = Orientation.Vertical,
                Margin = new Thickness(15, 30, 15, 30)
            };

            // 컨테이너 공간의 제목 표시줄
            StackPanel ctTitle = new StackPanel()
            {
                Name = "ctTitle" + idx.ToString(),
                Orientation = Orientation.Horizontal
            };
            TextBox title = new TextBox()
            {
                Text = "컨테이너" + idx.ToString()
            };
            Button btn_add = new Button() { Width = 50, Height = 50, Content = "추가" };
            Button btn_remove = new Button() { Width = 50, Height = 50, Content = "삭제" };
            ctTitle.Children.Add(title);
            ctTitle.Children.Add(btn_add);
            ctTitle.Children.Add(btn_remove);

            // 컨테이너 공간 안의 리스트 뷰
            ListView itemList = new ListView()
            {
                Name = "ctItemList" + idx.ToString(),

            };

            // 컨테이너 공간에 두 요소 장착
            ctSpace.Children.Add(ctTitle);
            ctSpace.Children.Add(itemList);

            // 컨테이너 공간을 컨테이너 리스트에 장착
            ctList.Children.Add(ctSpace);

        }
    }
}
