# MultiMedia

#### 介绍
多媒体相关
目前已封装ffmpeg rtmp推流

#### 软件架构
.NET 5



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

