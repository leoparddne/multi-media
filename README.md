# MultiMedia

#### 介绍
多媒体相关
目前已封装ffmpeg rtmp推流

#### 软件架构
.NET 5


核心逻辑为通过ffmpeg相关参数推流
```
-thread_queue_size 1000
-r 30 -f gdigrab
-s {width}x{height}
-offset_x {offsetPosstion.X}
-offset_y {offsetPosstion.Y}
-i desktop -vcodec libx264 -acodec copy -preset:v ultrafast -tune:v zerolatency -max_delay 10 -g 50 -sc_threshold 0 -f flv
```

通过绑定屏幕宽高及窗口定位抓取屏幕指定区域并将相关数据推流道指定rtmp服务器



#### 使用说明
//推流
```
            FFMpegRtmpPusher pusher = new FFMpegRtmpPusher();

            string testRtmp = "rtmp://xxxx";//rtmp地址
            //推送
            pusher.PushDesktop(SystemParameters.PrimaryScreenWidth,
                SystemParameters.PrimaryScreenHeight, new System.Drawing.Point(), testRtmp);
```
//停止
```
        pusher?.Kill();
```

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request

