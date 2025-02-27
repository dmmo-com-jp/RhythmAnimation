using Vortice.Direct2D1;
using Vortice.Direct2D1.Effects;
using Vortice.DirectWrite;
using YukkuriMovieMaker.Commons;
using YukkuriMovieMaker.Player.Video;

namespace RhythmAnimation
{
    internal class RhythmOpacityProcessor : IVideoEffectProcessor
    {
        readonly RhythmOpacity item;
        ID2D1Image? input;

        public ID2D1Image Output => input ?? throw new NullReferenceException(nameof(input) + "is null");

        public RhythmOpacityProcessor(RhythmOpacity item)
        {
            this.item = item;
        }

        /// <summary>
        /// エフェクトを更新する
        /// </summary>
        /// <param name="effectDescription">エフェクトの描画に必要な各種情報</param>
        /// <returns>描画位置等の情報</returns>
        public DrawDescription Update(EffectDescription effectDescription)
        {
            var frame = effectDescription.ItemPosition.Frame;
            var length = effectDescription.ItemDuration.Frame;
            var fps = effectDescription.FPS;
            var opacity = item.Opacity.GetValue(frame, length, fps)/100.0;
            var BPM = item.BPM.GetValue(frame, length, fps);
            var drawDesc = effectDescription.DrawDescription;
            var 拡大間隔 = Math.Round(1 / (BPM / 60.00) / (1.00 / fps));
            var fps_frame = frame % 拡大間隔;
            return
                drawDesc with
                {
                    Opacity = drawDesc.Opacity + ((float)opacity / (float)拡大間隔) * (float)fps_frame
                };
        }
        public void ClearInput()
        {
            input = null;
        }
        public void SetInput(ID2D1Image? input)
        {
            this.input = input;
        }

        public void Dispose()
        {

        }

    }
}
