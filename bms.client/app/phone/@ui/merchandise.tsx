import Image from "next/image";
import NullImg from "@/assets/img/nullImg.png";

export default function Merchandise() {
	return (
		<div className="border-2 rounded h-60 flex flex-col">
			<div className="basis-full">
				<Image alt="" priority className="h-full w-full" src={NullImg}></Image>
			</div>
			<div className="border-t text-sm p-1">
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
