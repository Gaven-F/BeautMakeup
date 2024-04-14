export default function Header() {
	return (
		<div className="full sticky top-0 text-center z-10 h-20 flex flex-col">
			<div className="bg-secondary-50 h-4"></div>
			<div className="basis-full bg-primary-100 flex">
				<h1 className="m-auto text-3xl font-black text-transparent bg-clip-text bg-[linear-gradient(135deg,#5b247a,#1bcedf)]">
					AI智能美妆商城
				</h1>
			</div>
			<div className="bg-secondary-50 h-4"></div>
		</div>
	);
}
