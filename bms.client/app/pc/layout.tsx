export default function PcLayout({
	children,
	header,
	menu,
}: {
	children: React.ReactNode;
	header: React.ReactNode;
	menu: React.ReactNode;
}) {
	return (
		<div className="flex flex-col h-full gap-2">
			<div className="h-20">{header}</div>
			<div className="flex flex-row basis-full gap-2">
				<div className="w-60 flex-none">{menu}</div>
				<div className="flex-1 w-0">{children}</div>
			</div>
		</div>
	);
}
