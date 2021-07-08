using System;
using System.Diagnostics;
using System.Drawing;

namespace MultiMedia.RtmpPusher
{
    //!!! FFMPEG的所有输出信息,都为错误输出流
    public class FFMpegRtmpPusher : Command.CommandBase
    {
        double width, height;
        Point offsetPosstion;
        string rtmp;
        public FFMpegRtmpPusher() : base("")
        {
            base.programe = AppDomain.CurrentDomain.BaseDirectory + "Lib\\ffmpeg";
        }

        /// <summary>
        /// 投屏
        /// </summary>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="offsetPosstion">屏幕偏移</param>
        public FFMpegRtmpPusher PushDesktop(double width, double height, Point offsetPosstion,string rtmp)
        {
            this.width = width;
            this.height = height;
            this.offsetPosstion = offsetPosstion;
            this.rtmp = rtmp;

            InjectParameter();
            Exec();
            return this;
        }

        public void InjectParameter()
        {
            //            gdigrab 录屏
            //-s 1920x1080 录制的屏幕宽度、高度
            //- offset_x 100  偏移
            // - offset_y 200  偏移
            //  需要注意宽度、高度分别加上偏移以后不能超出屏幕
            //例如 - s 1920x1080 - offset_x 100 - offset_y 200
            //会有以下报错
            //Capture area(10, 20),(1930, 1100) extends outside window area(0,-1440),(2560, 1080)desktop: I / O error
            //   - i desktop 录制屏幕
            //-thread_queue_size 此选项设置从文件或设备读取时排队数据包的最大数量。低延迟 / 高速率的直播流，如果不及时读取数据包可能会被丢弃；设置此值可以强制 ffmpeg 使用单独的输入线程并在数据包到达时立即读取数据包。默认情况下，ffmpeg 仅在指定了多个输入时才执行此操作。
            //-r 帧率
            //libx264 使用 libx264 编码所有视频流并复制所有音频流。
            //-acodec 设置音频编解码器,copy: 所选流的数据包应从输入文件传送并在输出文件中混合
            //- f 强制输入或输出文件格式。通常会自动检测输入文件的格式，并根据输出文件的文件扩展名猜测格式，因此在大多数情况下不需要此选项。

            AddParameter("-thread_queue_size 1000");
            AddParameter("-r 30 -f gdigrab");
            AddParameter($"-s {width}x{height}");
            AddParameter($"-offset_x {offsetPosstion.X}");
            AddParameter($"-offset_y {offsetPosstion.Y}");
            AddParameter(" -i desktop -vcodec libx264 -acodec copy -preset:v ultrafast -tune:v zerolatency -max_delay 10 -g 50 -sc_threshold 0 -f flv ");

            AddParameter(rtmp);
        }

        public override void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
        }

        public override void Process_Exited(object sender, EventArgs e)
        {
        }

        public override void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            //
            if (e.Data != null && e.Data.Contains("Error"))
            {

            }
        }
    }
}
