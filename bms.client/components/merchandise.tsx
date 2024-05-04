import Image from "next/image";
import NullImg from "@/assets/img/nullImg.png";

export default function Merchandise() {
	return (
		<div className="border-2 h-60 rounded flex flex-col">
			<div className="basis-full relative">
				<Image alt="" priority className="h-full w-full" fill src={NullImg}></Image>
			</div>
			<div className="flex-1 shrink-0 border-t text-sm p-1">
				<div className="flex items-end justify-between">
					<h3 className="text-base font-bold">XX商品</h3>
					<span className="text-stone-600">￥XX.XX</span>
				</div>
				<div className="flex justify-between font-light">
					<span>好评：XXX</span>
					<span>浏览量：XXX</span>
				</div>
			</div>
		</div>
	);
}
