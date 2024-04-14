const funMenu = [
	{ label: "首页", icon: "home", path: "/phone" },
	{ label: "商店", icon: "home", path: "/phone/shorp" },
	{ label: "个人中心", icon: "home", path: "/phone/personalCenter" },
];

export default function footer() {
	return (
		<div className="flex flex-row justify-around items-center h-full bg-primary-900 text-white ">
			{funMenu.map((it) => (
				<span key={it.path} className="w-20 text-center">
					{it.label}
				</span>
			))}
		</div>
	);
}
