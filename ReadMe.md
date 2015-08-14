# Windows 10 fro developer

Windows Universal Platform 開發Demo。帶各位體驗如何以最新的Visual Studio 2015 來開發UWP。在一個one app的目標下，如何在進行UI的調整，如何實現不同設備特定的功能，以及現有的8.1 Apps如何移植到Windows 10。

#UWP

使用.NET 技術來建立在所有 Windows 裝置上的 Universal Windows Platform 通用應用程式，這些 Windows 裝置可能是手機、平板或筆記型電腦、桌上的 PC、Xbox、HoloLens、Surface Hub 、以及 IoT 裝置如 Raspberry Pi 2 等。

one package one store

85% Common API

Extensions API for special device

# Adaptive UI 自適應式介面應用範例
UWP在one app one store的目標前題下，如何做出在各種類型的裝置上良好的使用者UI體驗是一個相對重要的課題，開發人員必須開發自適應式（adaptive UI）操作介面來適用於各種類型的裝置

以下程式碼僅列出片段，詳細程式碼請參考完整專案檔案

## SplitViewDemo - 新UI控制項 "漢堡選單"
Windows 10 APP 大量常見被使用的選單控制項

## TailoredDesignDemo - Devices 專屬頁面

**Sample Code 情境**

Device 尺吋小於7吋時，導向專屬頁面,於app.xaml OnLaunched 時動態決定主頁檔案

		var currentview = DisplayInformation.GetForCurrentView();
        var width = Window.Current.Bounds.Width * (int)currentview.ResolutionScale / 100;
        var height = Window.Current.Bounds.Height * (int)currentview.ResolutionScale / 100;
        var dpi = DisplayInformation.GetForCurrentView().RawDpiY;
        var screenDiagonal = Math.Sqrt(Math.Pow(width / dpi, 2) + Math.Pow(height / dpi, 2));

        if (screenDiagonal <= 7)
            rootFrame.Navigate(typeof(MainPageForMobile), e.Arguments);
        else
            rootFrame.Navigate(typeof(MainPage), e.Arguments);


## AdaptiveUIDemo - Back Button
**Sample Code 情境**

手機設備有back button實體鍵，因此APP不提供退回鍵，但在PC設備上沒有back button實體鍵，因此APP提供退回鍵UI

		var currentView = SystemNavigationManager.GetForCurrentView();

        if (!ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
        {
            currentView.AppViewBackButtonVisibility = this.Frame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        currentView.BackRequested += CurrentView_BackRequested;




## VisualStateManagerDemo - 依設備尺吋響應式UI
**Sample Code 情境**
依設備尺吋條件觸發字型大小變化

	<VisualStateManager.VisualStateGroups>
            <VisualStateGroup>
                <VisualState x:Name="wide">
                    <VisualState.StateTriggers>
                        <AdaptiveTrigger MinWindowWidth="1000"></AdaptiveTrigger>
                    </VisualState.StateTriggers>
                </VisualState>
                <VisualState x:Name="mid">
                    <VisualState.StateTriggers>
                        <AdaptiveTrigger MinWindowWidth="501"></AdaptiveTrigger>
                    </VisualState.StateTriggers>
                    <VisualState.Setters>
                        <Setter Target="textBlock.FontSize" Value="20"></Setter>
                    </VisualState.Setters>
                </VisualState>
                <VisualState x:Name="min">
                    <VisualState.StateTriggers>
                        <AdaptiveTrigger MinWindowWidth="0"></AdaptiveTrigger>
                    </VisualState.StateTriggers>
                    <VisualState.Setters>
                        <Setter Target="textBlock.FontSize" Value="12"></Setter>
                    </VisualState.Setters>
                </VisualState>
            </VisualStateGroup>
        </VisualStateManager.VisualStateGroups>


## RelativePanelDemo - 新UI控制項 "UI相對位置排版"
**Sample Code 情境**

結合VisualStateManager依設備尺吋條件觸發UI相對位置變化，達到適應式UI調整

	<RelativePanel Margin="100,50,100,50">
            <StackPanel x:Name="stackPanel1" Orientation="Vertical" Width="200" Margin="10" Height="200" 
                        Background="LightBlue">
                <TextBlock x:Name="textBlock" TextWrapping="Wrap" Text="白天-晴朗"/>
                <TextBlock x:Name="textBlock2" TextWrapping="Wrap" Text="晚上-多雲涼爽"/>
            </StackPanel>
            <StackPanel x:Name="stackPanel2" Orientation="Vertical" Width="200" Margin="10" Height="200" 
                        Background="LightCoral" RelativePanel.RightOf="stackPanel1">
                <TextBlock x:Name="textBlock3" TextWrapping="Wrap" Text="日出-上午 05:05"/>
                <TextBlock x:Name="textBlock4" TextWrapping="Wrap" Text="日落-下午 06:33"/>
            </StackPanel>
            <StackPanel x:Name="stackPanel3" Orientation="Vertical" Width="200" Height="200" Margin="10" 
                        Background="LightGreen" RelativePanel.RightOf="stackPanel2" >
                <TextBlock x:Name="textBlock5" TextWrapping="Wrap" Text="月出-上午 01:05"/>
                <TextBlock x:Name="textBlock6" TextWrapping="Wrap" Text="月落-下午 03:13"/>
            </StackPanel>
        </RelativePanel>

# Adaptive Code 應用範例
UWP架構下提供85%通用API集合可以在不同裝置間共用多數的程式碼，而面對特定設備獨有的特性，則可以採用Extension SDK的方式，呼叫指定平台的APIs 來撰寫自適應式程式碼

以下程式碼僅列出片段，詳細程式碼請參考完整專案檔案
## AdaptiveCodeDemo - Extension SDK
**Sample Code 情境**

手機設備上有StatusBar UI，而pc設備則沒有，為求一致性UI呈現，因此當APP運行在手機設備上時進行StatusBar UI的隱藏

手機設備上具有硬體實體按鍵，而pc設備則沒有，當APP運行在手機設備上時提供照相按鍵功能

	//當APP運行在手機設備上時進行StatusBar UI的隱藏
	if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
    {
        Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
    }

	//當APP運行在手機設備上時提供照相按鍵功能
    if (Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
    {
        Windows.Phone.UI.Input.HardwareButtons.CameraPressed += HardwareButtons_CameraPressed;
    }

# Windows 8.1 UAP移植到Windows 10 UWP

1) Adaptive Code 進行平台API的判斷處理

2) 使用其它控制項取代Charm Bar，例如SplitView

3) 設計自適應UI

4) Shared Project 程式碼轉移至 UWP Project

[https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954974.aspx](https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954974.aspx "将 Universal 8.1 App 移动到 Windows 10 Insider Preview")

[https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954971.aspx](https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954971.aspx "案例研究：Bookstore1")

[https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954972.aspx](https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954972.aspx "案例研究：Bookstore2")

[https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954973.aspx](https://msdn.microsoft.com/zh-cn/library/windows/apps/dn954973.aspx "案例研究：QuizGame 对等示例应用")
